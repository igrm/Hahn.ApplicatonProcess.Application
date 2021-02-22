import { inject, customAttribute } from 'aurelia-framework';
import $ from 'jquery';
import { SettingsService } from '../services/settings-service';

@customAttribute('datepicker')
@inject(Element, SettingsService)

export class DatePicker {
    constructor(private element: Element, private settingsService: SettingsService) {
    }

    attached() {
        ($(this.element) as any).datepicker({
            format: this.settingsService.defaultDateFormat.toLowerCase(),
            todayHighlight: true,
            language: this.settingsService.defaultLocale,
            autoclose:true
        }).on("hide", (e) => { 
            if (e.target.value && e.target.value.length > 0) {
                const event = new Event('change');
                e.target.dispatchEvent(event);
            }
        }).on("changeDate", (e) => {
            if (e.target.value && e.target.value.length > 0) {
                const event = new Event('change');
                e.target.dispatchEvent(event);
            }
        });
    }
}
