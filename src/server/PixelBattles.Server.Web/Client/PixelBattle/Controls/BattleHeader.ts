export class BattleHeader{
    private headerContainer: HTMLDivElement;

    constructor(headerContainer: HTMLDivElement, text: string) {
        this.headerContainer = headerContainer;
        this.headerContainer.className = "card-header";
        let header: HTMLHeadingElement = <HTMLHeadingElement>document.createElement('h4');
        header.className = "my-0 font-weight-normal";
        header.textContent = text;
        this.headerContainer.appendChild(header)
    }

    get height(): number {
        return this.headerContainer.offsetHeight;
    }
}