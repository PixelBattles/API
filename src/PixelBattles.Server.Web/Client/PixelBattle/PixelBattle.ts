import { ApiClient } from "./Clients/ApiClient";
import { IApiClient } from "./Clients/IApiClient";
import { BattleHeader } from "./Controls/BattleHeader";
import { BattleBody } from "./Controls/BattleBody";

export class PixelBattle {
    private apiClient: IApiClient;

    private battleId: string;
    private widgetContainer: HTMLDivElement;

    private header: BattleHeader;
    private body: BattleBody;
    
    constructor(widgetContainer: HTMLDivElement) {
        this.widgetContainer = widgetContainer;
        this.widgetContainer.className = "card";
        this.battleId = this.widgetContainer.getAttribute("battle-id");

        this.header = this.initializeHeader("test header text");
        this.body = this.initializeBody();

        window.onresize = this.resize;
        window.onload = this.resize;

        this.apiClient = new ApiClient("/api/");
    }

    private initializeHeader(text: string): BattleHeader{
        let headerContainer: HTMLDivElement = <HTMLDivElement>document.createElement('div');
        this.widgetContainer.appendChild(headerContainer);
        return new BattleHeader(headerContainer, text);
    }

    private initializeBody(): BattleBody {
        let canvasContainer: HTMLDivElement = <HTMLDivElement>document.createElement('div');
        this.widgetContainer.appendChild(canvasContainer);
        return new BattleBody(canvasContainer);
    }

    public resize = (ev: Event): void => {
        this.body.resize(this.widgetContainer.offsetWidth - 2/*borders*/, this.widgetContainer.offsetHeight - this.header.height - 2/*borders*/);
    }
}