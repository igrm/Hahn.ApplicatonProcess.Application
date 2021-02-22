import { __decorate, __metadata } from "tslib";
import { bindable, computedFrom } from 'aurelia-framework';
import { inject, NewInstance } from 'aurelia-dependency-injection';
import { ValidationController, ValidationRules } from 'aurelia-validation';
import { BootstrapFormRenderer } from '../common/bootstrap-form-renderer';
import { LovService } from '../services/lov-service';
import moment from 'moment';
import { SettingsService } from '../services/settings-service';
import $ from 'jquery';
import { DialogService } from 'aurelia-dialog';
import { Prompt, DialogModel, DialogStyle } from '../prompt/prompt';
import { Router } from 'aurelia-router';
import { Spinner } from 'spin.js';
import { DataService, Asset } from '../services/data-service';
let AssetInput = class AssetInput {
    constructor(assetValidationController, bootstrapFormRenderer, lovService, settingsService, dialogService, router, dataService) {
        this.assetValidationController = assetValidationController;
        this.dateFormat = settingsService.defaultDateFormat;
        this.dialogService = dialogService;
        this.router = router;
        this.dataService = dataService;
        const assetValidationRules = ValidationRules
            .ensure((res) => res.assetName).displayName('keyAssetName').required()
            .ensure((res) => res.assetName).minLength(5)
            .ensure((res) => res.department).displayName('keyDepartment').required()
            .ensure((res) => res.country).displayName('keyCountry').required()
            .ensure((res) => res.purchaseDate).displayName('keyPurchaseDate').required()
            .ensure((res) => res.purchaseDate).satisfies((value) => { return value ? moment(value, this.dateFormat).isValid() : true; }).withMessageKey("invalidValue")
            .ensure((res) => res.purchaseDate).displayName('keyPurchaseDate').satisfies((value) => { return value && moment(value, this.dateFormat).isValid() ? moment(value, this.dateFormat).isBetween(moment().subtract(1, "years"), moment()) : true; }).withMessageKey("oneYear")
            .ensure((res) => res.email).displayName('keyEmail').required().email()
            .rules;
        this.assetValidationController.addRenderer(bootstrapFormRenderer);
        this.assetValidationController.addObject(this, assetValidationRules);
        this.departmentsLov = lovService.departments;
        this.countriesLov = lovService.countries;
    }
    attached() {
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
    }
    submit() {
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
        asset.broken = this.broken;
        $("#dimm").show();
        this.dataService.SendAsset(asset).then((response) => {
            $("#dimm").hide();
            if (response.isSuccess) {
                this.router.navigateToRoute("confirm");
            }
            else {
                this.dialogService.open({
                    viewModel: Prompt, model: model, lock: false
                });
            }
        }).catch(() => {
            $("#dimm").hide();
            this.dialogService.open({
                viewModel: Prompt, model: model, lock: false
            });
        });
    }
    reset() {
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
    get formIsValid() {
        let result = true;
        let json = $(".form-asset").attr('data-map');
        if (json) {
            let map = JSON.parse(json);
            map.forEach((x) => {
                if (x.length == 0)
                    return result = result && false;
                x.forEach((y) => {
                    result = result && Boolean(y);
                });
            });
        }
        return result;
    }
    get formIsFilled() {
        return this.assetName || this.department || this.country || this.purchaseDate || this.email || (this.broken);
    }
    get errorMessagesExist() {
        return $(".invalid-feedback ").length > 0;
    }
    discardFormState() {
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
};
__decorate([
    bindable,
    __metadata("design:type", String)
], AssetInput.prototype, "assetName", void 0);
__decorate([
    bindable,
    __metadata("design:type", String)
], AssetInput.prototype, "department", void 0);
__decorate([
    bindable,
    __metadata("design:type", String)
], AssetInput.prototype, "country", void 0);
__decorate([
    bindable,
    __metadata("design:type", Date)
], AssetInput.prototype, "purchaseDate", void 0);
__decorate([
    bindable,
    __metadata("design:type", String)
], AssetInput.prototype, "email", void 0);
__decorate([
    bindable,
    __metadata("design:type", Boolean)
], AssetInput.prototype, "broken", void 0);
__decorate([
    bindable,
    __metadata("design:type", Map)
], AssetInput.prototype, "departmentsLov", void 0);
__decorate([
    bindable,
    __metadata("design:type", Map)
], AssetInput.prototype, "countriesLov", void 0);
__decorate([
    bindable,
    __metadata("design:type", Object)
], AssetInput.prototype, "dateFormat", void 0);
__decorate([
    computedFrom("assetName", "department", "country", "purchaseDate", "email", "broken"),
    __metadata("design:type", Boolean),
    __metadata("design:paramtypes", [])
], AssetInput.prototype, "formIsFilled", null);
AssetInput = __decorate([
    inject(NewInstance.of(ValidationController), NewInstance.of(BootstrapFormRenderer), LovService, SettingsService, DialogService, Router, DataService),
    __metadata("design:paramtypes", [ValidationController, BootstrapFormRenderer,
        LovService, SettingsService, DialogService, Router, DataService])
], AssetInput);
export { AssetInput };
//# sourceMappingURL=assetinput.js.map