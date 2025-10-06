export class ErrorHandler {
    private errorDisplay: HTMLElement;
    private haveErrors: boolean;

    constructor (errorDisplay: HTMLElement) {
        this.errorDisplay = errorDisplay;
        this.haveErrors = false;
        return this;
    }

    displayError(errorMessage: string, elements: HTMLElement[]): void {
        elements.forEach(element => {
            element.classList.add('error');
        });

        this.haveErrors = true;
        this.errorDisplay.style.color = 'red';
        this.errorDisplay.textContent = errorMessage;
        this.errorDisplay.classList.add('error');
    }

    displaySuccess(): void {
        this.errorDisplay.style.color = 'green';
        this.errorDisplay.textContent = 'success';
        this.errorDisplay.classList.add('success');
        console.log('success');
    }

    hideErrors(): void {
        if (this.haveErrors === false)
            return;

        this.errorDisplay.textContent = '';
        let errors: NodeListOf<Element> = document.querySelectorAll('.error');

        // for (let i: number = 0; i < errors.length; i++)
        //     errors[i]!.classList.remove('error');

        errors.forEach(error => error.classList.remove('error'))
    }
}
