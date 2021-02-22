import { RouterConfiguration, Router } from 'aurelia-router';
import { PLATFORM, inject } from "aurelia-framework";
import { I18N } from 'aurelia-i18n';
import { ValidationMessageProvider } from 'aurelia-validation';
import { SettingsService } from './services/settings-service';

@inject(I18N, SettingsService)

export class App {

    router: Router;

    constructor(private i18n, private settingsService) {
        this.i18n.setLocale(settingsService.defaultLocale);

        ValidationMessageProvider.prototype.getMessage = function (key) {
            const translation = i18n.tr(`errorMessages.${key}`);
            return this.parser.parse(translation);
        };

        ValidationMessageProvider.prototype.getDisplayName = function (propertyName, displayName) {
            return i18n.tr(displayName ? displayName : propertyName);
        };

    }

    configureRouter(config: RouterConfiguration, router: Router): void {

        this.router = router;

        config.map([
            { route: ['', 'input'], name: 'assetinput', title: this.i18n.tr("keyTitle"), moduleId: PLATFORM.moduleName('assetinput/assetinput'), nav: true },
            { route: ['confirm'], name: 'confirm', title: this.i18n.tr("keyThankYou"), moduleId: PLATFORM.moduleName('confirm/confirm'), nav: true },
        ]);
    }


}