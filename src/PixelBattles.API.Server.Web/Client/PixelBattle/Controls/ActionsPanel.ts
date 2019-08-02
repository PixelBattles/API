export class ActionsPanel{
    private container: HTMLDivElement;

    constructor(container: HTMLDivElement) {
        this.container = container;
        let actionsPanel: HTMLHeadingElement = <HTMLHeadingElement>document.createElement('div');
        actionsPanel.id = "actions-panel";
        actionsPanel.setAttribute("style", "height: 45px; width: 100%; background-color: yellow;");
        this.container.appendChild(actionsPanel)
    }
}