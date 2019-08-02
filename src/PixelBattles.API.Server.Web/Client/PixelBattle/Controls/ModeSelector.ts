export class ModeSelector{
    private container: HTMLDivElement;

    constructor(container: HTMLDivElement) {
        this.container = container;
        let modeSelector: HTMLHeadingElement = <HTMLHeadingElement>document.createElement('div');
        modeSelector.id = "mode-selector";
        modeSelector.setAttribute("style", "height: 45px; width: 100%; background-color: green;");
        this.container.appendChild(modeSelector)
    }
}