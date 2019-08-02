import { ColorPalette } from "./ColorPalette";
import { ActionsPanel } from "./ActionsPanel";
import { ModeSelector } from "./ModeSelector";

export class BattleFooter{
    private container: HTMLDivElement;
    private footer: HTMLDivElement;
    private topRow: HTMLDivElement;
    private bottomRow: HTMLDivElement;
    private colorPalette: ColorPalette;
    private modeSelector: ModeSelector;
    private actionsPanel: ActionsPanel;

    constructor(container: HTMLDivElement) {
        this.container = container;
        this.footer = <HTMLDivElement>document.createElement('div');
        this.footer.id = "footer";
        this.footer.setAttribute("style", "height: 90px; width: 100%; position: fixed; z-index: 1; bottom: 0; background-color: rgba(0, 0, 0, 0.7); display: flex; flex-direction: column; justify-content: flex-start; align-items: center;");
        this.container.appendChild(this.footer)

        this.topRow = <HTMLDivElement>document.createElement('div');
        this.topRow.id = "footer-top-row";
        this.topRow.setAttribute("style", "height: 45px; width: 100%; display: flex; flex-direction: row; justify-content: center; align-items: center;");
        this.footer.appendChild(this.topRow)

        this.bottomRow = <HTMLDivElement>document.createElement('div');
        this.bottomRow.id = "footer-bottom-row";
        this.bottomRow.setAttribute("style", "height: 45px; width: 100%; display: flex; flex-direction: row; justify-content: center; align-items: center;");
        this.footer.appendChild(this.bottomRow)

        this.colorPalette = this.initializeColorPalette();
        this.modeSelector = this.initializeModeSelector();
        this.actionsPanel = this.initializeActionsPanel();
    }
    
    private initializeColorPalette(): ColorPalette {
        
        return new ColorPalette(this.topRow);
    }

    private initializeModeSelector(): ModeSelector {
        return new ModeSelector(this.bottomRow);
    }

    private initializeActionsPanel(): ActionsPanel {
        return new ActionsPanel(this.bottomRow);
    }
}