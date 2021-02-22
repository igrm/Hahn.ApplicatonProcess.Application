import { bindable } from 'aurelia-framework';
import { inject, NewInstance } from 'aurelia-dependency-injection';
import $ from 'jquery';

export class Dropdown {
    @bindable public caption: String;
    @bindable public id: String;
    @bindable public value: String;
    @bindable public lov: Map<String, String>;
    @bindable public tabindex: Number;
    @bindable public dependent: String;

    constructor() {
        ($.expr[':'] as any).contains =  (a, i, m) => {
            return $(a).text().toUpperCase()
                .indexOf(m[3].toUpperCase()) >= 0;
        };
    }

    attached(): void {
        $(`#${this.id}`).on('shown.bs.dropdown', (e: any) => {
            let parent = $(e.target).parent();
            parent.find(".dropdown-header").hide();
            parent.find(".dropdown-item").show();

            let input = parent.find("input[type='search']");
            input.val('');
            
            input.focus();

            input.on('input', (e: any) => {
                let header = parent.find(".dropdown-header");
                if (e.target.value.length == 0) {
                    parent.find(".dropdown-item").show();
                    header.hide();
                }
                else {
                    parent.find(".default-lov-value").hide();
                    parent.find(".lov-value").hide();
                    parent.find(".lov-value:contains('" + e.target.value + "')").show();

                    if (parent.find(".lov-value:visible").length == 0) {
                        header.show();
                    }
                    else {
                        header.hide();
                    }
                }
               
            });
        });
    }

    click(e: any): void {
        let parent = $(e.target).closest(".dropdown");
        let button = parent.find("button");
        button.text($(e.target).text());
        this.value = $(e.target).data('selected-value') as String;

        if (this.dependent) {
            var depententElement = $(`#${this.dependent}`);
            depententElement.val('');
            depententElement.closest(".form-group").find(".is-invalid").removeClass("is-invalid");
            depententElement.closest(".form-group").find(".custom-is-invalid").removeClass("custom-is-invalid");
            depententElement.closest(".form-group").find(".invalid-feedback ").remove();
        }
    }

    get getButtonId():string {
        return `${this.id}-button`;
    }
}