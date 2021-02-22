import { __decorate, __metadata } from "tslib";
import { inject, customAttribute } from 'aurelia-framework';
import $ from 'jquery';
import { SettingsService } from '../services/settings-service';
let DatePicker = class DatePicker {
    constructor(element, settingsService) {
        this.element = element;
        this.settingsService = settingsService;
    }
    attached() {
        $(this.element).datepicker({
            format: this.settingsService.defaultDateFormat.toLowerCase(),
            todayHighlight: true,
            language: this.settingsService.defaultLocale,
            autoclose: true
        }).on("hide", (e) => {
            if (e.target.value && e.target.value.length > 0) {
                const event = new Event('change');
                e.target.dispatchEvent(event);
            }
        });
    }
};
DatePicker = __decorate([
    customAttribute('datepicker'),
    inject(Element, SettingsService),
    __metadata("design:paramtypes", [Element, SettingsService])
], DatePicker);
export { DatePicker };
//# sourceMappingURL=datepicker.js.map