export class BattleFooter{
    private container: HTMLDivElement;

    constructor(container: HTMLDivElement) {
        this.container = container;
        let header: HTMLHeadingElement = <HTMLHeadingElement>document.createElement('div');
        header.id = "footer";
        header.setAttribute("style", "height: 90px; width: 100%; position: fixed; z-index: 1; bottom: 0;background-color: black; opacity: 0.5;");
        this.container.appendChild(header)
    }
}