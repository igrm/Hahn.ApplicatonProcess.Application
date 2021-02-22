import { bindable, computedFrom, PLATFORM } from 'aurelia-framework';
import { inject, NewInstance } from 'aurelia-dependency-injection';
import { ValidationController, ValidationRules, validateTrigger } from 'aurelia-validation';
import { BootstrapFormRenderer } from '../common/bootstrap-form-renderer';
import { LovService } from '../services/lov-service';
import moment from 'moment';
import { SettingsService } from '../services/settings-service';
import $ from 'jquery';
import { DialogService } from 'aurelia-dialog';

import { Prompt, DialogModel, DialogStyle } from '../prompt/prompt';
PLATFORM.moduleName('../prompt/prompt')

import { Router } from 'aurelia-router';
import { Spinner } from 'spin.js';
import { DataService, Asset } from '../services/data-service';
import { HttpResponseMessage } from 'aurelia-http-client';

@inject(NewInstance.of(ValidationController), NewInstance.of(BootstrapFormRenderer), LovService, SettingsService, DialogService, Router, DataService)

export class AssetInput {

    @bindable public assetName: string;
    @bindable public department: string;
    @bindable public country: string;
    @bindable public purchaseDate: Date;
    @bindable public email: string;
    @bindable public broken: boolean;

    @bindable public departmentsLov: Map<String, String>;
    @bindable public countriesLov: Map<String, String>;

    @bindable public dateFormat;

    assetValidationController: ValidationController;
    dialogService: DialogService;
    router: Router;
    dataService: DataService;
    lovService: LovService;
    settingService: SettingsService;

    constructor(assetValidationController: ValidationController, bootstrapFormRenderer: BootstrapFormRenderer,
        lovService: LovService, settingsService: SettingsService, dialogService: DialogService, router: Router, dataService: DataService) {

        this.assetValidationController = assetValidationController;
        this.dateFormat = settingsService.defaultDateFormat;
        this.dialogService = dialogService;
        this.router = router;
        this.dataService = dataService;
        this.settingService = settingsService;
        this.lovService = lovService;

        const assetValidationRules = ValidationRules
            .ensure((res: AssetInput) => res.assetName).displayName('keyAssetName').required()
            .ensure((res: AssetInput) => res.assetName).minLength(5)
            .ensure((res: AssetInput) => res.department).displayName('keyDepartment').required()
            .ensure((res: AssetInput) => res.country).displayName('keyCountry').required()
            .ensure((res: AssetInput) => res.purchaseDate).displayName('keyPurchaseDate').required()
            .ensure((res: AssetInput) => res.purchaseDate).satisfies((value: Date) => { return value ? moment(value, this.dateFormat).isValid() : true }).withMessageKey("invalidValue")
            .ensure((res: AssetInput) => res.purchaseDate).displayName('keyPurchaseDate').satisfies((value: Date) => { return value && moment(value, this.dateFormat).isValid() ? moment(value, this.dateFormat).isBetween(moment().subtract(1, "years"), moment()) : true }).withMessageKey("oneYear")
            .ensure((res: AssetInput) => res.email).displayName('keyEmail').required()
            .ensure((res: AssetInput) => res.email).email()
            .ensure((res: AssetInput) => res.email).satisfies((value: string) => {
                let result = false;
                if( !value
                    || value.length == 0
                    || !/^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/.test(value)) return !result;
                let domains = this.lovService.domains;
                domains.some((domain:string) => {
                    result = value.endsWith(domain);
                    return result;
                });
                return result;
            }).withMessageKey("wrongTopLevelDomain")
            .rules;

        this.assetValidationController.addRenderer(bootstrapFormRenderer);
        this.assetValidationController.addObject(this, assetValidationRules);
    }

    attached(): void {
        const opts = {
            lines: 14,
            length: 38,
            width: 6,
            radius: 45,
            scale: 1,
            corners: 1,
            speed: 1,
            rotate: 0,
            animation: 'spinner-line-fade-quick',
            direction: 1,
            color: '#ffffff',
            fadeColor: 'transparent',
            top: '50%',
            left: '50%',
            shadow: '0 0 1px transparent',
            zIndex: 2000000000,
            className: 'spinner',
            position: 'absolute',
        };

        let spinner = new Spinner(opts).spin($("#dimm")[0]);

        let model = new DialogModel();

        model.title = "keyErrorDialogTitle";
        model.message = "keyErrorDialogMessage";
        model.dialogStyle = DialogStyle.Ok;

        if (this.lovService.departments.size == 0 || this.lovService.countries.size == 0 || this.lovService.domains.length == 0) {
            $("#dimm").show();
            this.lovService.populateDepartments().then((x: HttpResponseMessage) => {
                if (x.isSuccess) {
                    localStorage.setItem(this.settingService.storageKeyDepartments, x.response);

                    this.lovService.populateCountries().then((x: HttpResponseMessage) => {
                        if (x.isSuccess) {
                            
                            localStorage.setItem(this.settingService.storageKeyCountries, x.response);

                            this.lovService.populateTopLevelDomains().then((x: HttpResponseMessage) => {
                                if (x.isSuccess) {

                                    localStorage.setItem(this.settingService.storageKeyTopLevelDomains, x.response);

                                    this.departmentsLov = this.lovService.departments;
                                    this.countriesLov = this.lovService.countries;

                                    $("#dimm").hide();
                                }
                                else {

                                    this.dialogService.open({
                                        viewModel: Prompt, model: model, lock: false
                                    }).whenClosed(response => {
                                        $("#dimm").hide();
                                    });
                                }
                            }).catch((x) => {
                                this.dialogService.open({
                                    viewModel: Prompt, model: model, lock: false
                                }).whenClosed(response => {
                                    $("#dimm").hide();
                                });
                            });
                        }
                        else {

                            this.dialogService.open({
                                viewModel: Prompt, model: model, lock: false
                            }).whenClosed(response => {
                                $("#dimm").hide();
                            });
                        }
                    })
                        .catch((x) => {
                            this.dialogService.open({
                                viewModel: Prompt, model: model, lock: false
                            }).whenClosed(response => {
                                $("#dimm").hide();
                            });
                        });
                }
                else {
                    this.dialogService.open({
                        viewModel: Prompt, model: model, lock: false
                    }).whenClosed(response => {
                        $("#dimm").hide();
                    });
                }
            }).catch((x) => {
                this.dialogService.open({
                    viewModel: Prompt, model: model, lock: false
                }).whenClosed(response => {
                    $("#dimm").hide();
                });
            });
        }
        else {
            this.departmentsLov = this.lovService.departments;
            this.countriesLov = this.lovService.countries;
        }
    }

    public submit(): void {

        let model = new DialogModel();

        model.title = "keyErrorDialogTitle";
        model.message = "keyErrorDialogMessage";
        model.dialogStyle = DialogStyle.Ok;

        let asset = new Asset();

        asset.assetName = this.assetName;
        asset.department = this.department;
        asset.country = this.country;
        asset.purchaseDate = this.purchaseDate;
        asset.email = this.email;

        if (this.broken !== undefined && this.broken)
            asset.broken = `${this.broken}`;

        $("#dimm").show();
        this.dataService.SendAsset(asset).then((response) => {
            if (response.isSuccess) {
                $("#dimm").hide();
                this.router.navigateToRoute("confirm");
            }
            else {
                this.dialogService.open({
                    viewModel: Prompt, model: model, lock: false
                }).whenClosed(response => {
                    $("#dimm").hide();
                });;
            }
        }).catch(() => {
            this.dialogService.open({
                viewModel: Prompt, model: model, lock: false
            }).whenClosed(response => {
                $("#dimm").hide();
            });
        });
    }

    public reset(): void {

        let model = new DialogModel();
        model.title = "keyResetDialogTitle";
        model.message = "keyResetDialogMessage";
        model.dialogStyle = DialogStyle.YesNo;

        this.dialogService.open({
            viewModel: Prompt, model: model, lock: false
        }).whenClosed(response => {
            if (!response.wasCancelled) {
                this.discardFormState();
            }
        });
    }

    get formIsValid(): boolean {

        let result: boolean = true;

        let json = $(".form-asset").attr('data-map');
        if (json) {
            let map = JSON.parse(json) as Array<Array<Number>>;
            map.forEach((x: Array<Number>) => {
                if (x.length == 0) return result = result && false;
                x.forEach((y: Number) => {
                    result = result && Boolean(y);
                })
            });
        }
        return result;
    }

    @computedFrom("assetName", "department", "country", "purchaseDate", "email", "broken")
    get formIsFilled(): boolean {
        return (this.assetName as any) || (this.department as any) || (this.country as any) || (this.purchaseDate as any) || (this.email as any) || (this.broken);
    }


    get errorMessagesExist() {
        return $(".invalid-feedback ").length > 0
    }

    private discardFormState(): void {


        this.assetName = null;
        this.department = null;
        this.country = null;
        this.purchaseDate = null;
        this.email = null;
        this.broken = null;

        $("input[type='text']").val('');
        $("input[type='hidden']").val('');
        $("input[type='email']").val('');
        $("input[type='checkbox']").prop('checked', false);

        $("#department-button").text($("#department-button").parent().find(".default-lov-value").text());
        $("#country-button").text($("#country-button").parent().find(".default-lov-value").text());

        $(".is-invalid").removeClass("is-invalid");
        $(".custom-is-invalid").removeClass("custom-is-invalid");
        $(".invalid-feedback ").remove();

        $(".form-asset").attr("data-map", "[[],[],[],[],[]]");
        $("input[type='submit']").attr("disabled", "disabled");
        $("input[type='reset']").attr("disabled", "disabled");
    }
}