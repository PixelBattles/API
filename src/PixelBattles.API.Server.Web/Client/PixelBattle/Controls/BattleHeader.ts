export class BattleHeader{
    private container: HTMLDivElement;

    constructor(container: HTMLDivElement, text: string) {
        this.container = container;
        let header: HTMLDivElement = <HTMLDivElement>document.createElement('div');
        header.id = "header";
        header.setAttribute("style", "height: 45px; width: 100%; position: fixed; z-index: 1; top: 0; background-color: rgba(0, 0, 0, 0.7); display: flex; flex-direction: row; justify-content: space-between; align-items: center;");
        this.container.appendChild(header)
    }
}