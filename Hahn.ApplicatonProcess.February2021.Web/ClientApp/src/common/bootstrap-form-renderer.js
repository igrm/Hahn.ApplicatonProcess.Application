export class BootstrapFormRenderer {
    render(instruction) {
        for (let { result, elements } of instruction.unrender) {
            for (let element of elements) {
                this.remove(element, result);
            }
        }
        for (let { result, elements } of instruction.render) {
            for (let element of elements) {
                this.add(element, result);
            }
        }
    }
    mark(element, result, addOrRemove) {
        const form = element.closest('form');
        let json = form.attributes.getNamedItem("data-map").value;
        if (json) {
            let map = JSON.parse(json);
            let tabindex = element.getAttribute("tabindex");
            if (!map[tabindex]) {
                map[tabindex] = new Array();
            }
            if (addOrRemove == "add") {
                map[tabindex].push(Number(result.valid));
            }
            else if (addOrRemove == "remove") {
                map[tabindex].shift();
            }
            form.attributes.getNamedItem("data-map").value = JSON.stringify(map);
            //console.log(map);
        }
    }
    add(element, result) {
        //console.log(`add ${element.id} ${result.message} ${result.valid} ${result.id} ${result.rule.messageKey}`);
        this.mark(element, result, "add");
        const formGroup = element.closest('.form-group');
        if (!formGroup) {
            return;
        }
        if (!result.valid) {
            element.classList.add('is-invalid');
            var toggle = formGroup.querySelector('.dropdown-toggle');
            if (toggle) {
                toggle.classList.add('custom-is-invalid');
            }
            // add help-block
            const message = document.createElement('div');
            message.className = 'invalid-feedback display';
            message.textContent = result.message;
            message.id = `validation-message-${result.id}`;
            formGroup.appendChild(message);
        }
    }
    remove(element, result) {
        //console.log(`remove ${element.id} ${result.message} ${result.valid} ${result.id} ${JSON.stringify(result.rule.messageKey)}`);
        this.mark(element, result, "remove");
        const formGroup = element.closest('.form-group');
        if (!formGroup) {
            return;
        }
        if (!result.valid) {
            // remove help-block
            const message = formGroup.querySelector(`#validation-message-${result.id}`);
            if (message) {
                formGroup.removeChild(message);
                // remove the has-error class from the enclosing form-group div
                if (formGroup.querySelectorAll('.invalid-feedback').length === 0) {
                    element.classList.remove('is-invalid');
                    var toggle = formGroup.querySelector('.dropdown-toggle');
                    if (toggle) {
                        toggle.classList.remove('custom-is-invalid');
                    }
                }
            }
        }
    }
}
//# sourceMappingURL=bootstrap-form-renderer.js.map