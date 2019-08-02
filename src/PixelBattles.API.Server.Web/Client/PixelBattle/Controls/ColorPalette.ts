export class ColorPalette{
    private container: HTMLDivElement;

    constructor(container: HTMLDivElement) {
        this.container = container;
        let colorPalette: HTMLHeadingElement = <HTMLHeadingElement>document.createElement('div');
        colorPalette.id = "color-palette";
        colorPalette.setAttribute("style", "height: 45px; width: 100%; background-color: red;");
        this.container.appendChild(colorPalette)
    }
}