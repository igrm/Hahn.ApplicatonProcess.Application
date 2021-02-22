import { __decorate, __metadata } from "tslib";
import { bindable } from 'aurelia-framework';
import $ from 'jquery';
export class Dropdown {
    constructor() {
        $.expr[':'].contains = (a, i, m) => {
            return $(a).text().toUpperCase()
                .indexOf(m[3].toUpperCase()) >= 0;
        };
    }
    attached() {
        $(`#${this.id}`).on('shown.bs.dropdown', (e) => {
            let parent = $(e.target).parent();
            parent.find(".dropdown-header").hide();
            parent.find(".dropdown-item").show();
            let input = parent.find("input[type='search']");
            input.val('');
            input.focus();
            input.on('input', (e) => {
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
    click(e) {
        let parent = $(e.target).closest(".dropdown");
        let button = parent.find("button");
        button.text($(e.target).text());
        this.value = $(e.target).data('selected-value');
    }
    get getButtonId() {
        return `${this.id}-button`;
    }
}
__decorate([
    bindable,
    __metadata("design:type", String)
], Dropdown.prototype, "caption", void 0);
__decorate([
    bindable,
    __metadata("design:type", String)
], Dropdown.prototype, "id", void 0);
__decorate([
    bindable,
    __metadata("design:type", String)
], Dropdown.prototype, "value", void 0);
__decorate([
    bindable,
    __metadata("design:type", Map)
], Dropdown.prototype, "lov", void 0);
__decorate([
    bindable,
    __metadata("design:type", Number)
], Dropdown.prototype, "tabindex", void 0);
//# sourceMappingURL=dropdown.js.map