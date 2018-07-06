import { ApiClient } from "./Clients/ApiClient";
import { IApiClient } from "./Clients/IApiClient";
import { BattleHeader } from "./Controls/BattleHeader";
import { BattleCanvas } from "./Controls/BattleCanvas";

export class PixelBattle {
    private apiClient: IApiClient;

    private battleId: string;
    private widgetContainer: HTMLDivElement;

    private header: BattleHeader;
    private canvas: BattleCanvas;
    
    constructor(widgetContainer: HTMLDivElement) {
        this.widgetContainer = widgetContainer;
        this.widgetContainer.className = "card";
        this.battleId = this.widgetContainer.getAttribute("battle-id");

        this.header = this.initializeHeader("test header text");
        this.canvas = this.initializeCanvas();

        window.onresize = this.resize.bind(this);
        window.onload = this.resize.bind(this);

        this.apiClient = new ApiClient("/api/");
    }

    private initializeHeader(text: string): BattleHeader{
        let headerContainer: HTMLDivElement = <HTMLDivElement>document.createElement('div');
        this.widgetContainer.appendChild(headerContainer);
        return new BattleHeader(headerContainer, text);
    }

    private initializeCanvas(): BattleCanvas {
        let canvasContainer: HTMLDivElement = <HTMLDivElement>document.createElement('div');
        this.widgetContainer.appendChild(canvasContainer);
        return new BattleCanvas(canvasContainer);
    }

    public resize(ev: Event) {
        this.canvas.resize(this.widgetContainer.offsetWidth - 2/*borders*/, this.widgetContainer.offsetHeight - this.header.height - 2/*borders*/);
    }
}