export class BattleHeader{
    private container: HTMLDivElement;

    constructor(container: HTMLDivElement, text: string) {
        this.container = container;
        let header: HTMLHeadingElement = <HTMLHeadingElement>document.createElement('div');
        header.id = "toolbar";
        header.setAttribute("style", "height: 45px; width: 100%; position: absolute; z-index: 1; top: 0;background-color: black; opacity: 0.5;");
        this.container.appendChild(header)
    }
}