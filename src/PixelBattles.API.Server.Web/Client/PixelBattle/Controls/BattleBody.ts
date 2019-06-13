import { Chunk } from "./Client/Chunk";
import { IGameCanvas, GameCanvas, PositionEvent } from "./Client/GameCanvas";
import { ICamera, Camera } from "./Client/Camera";
import { IRenderEngine, RenderEngine } from "./Client/RenderEngine";
import { IBattle } from "../Clients/IApiClient";
import { IHubClient } from "../Clients/IHubClient";
import { HubClient } from "../Clients/HubClient";
import { IChunkGrid, ChunkGrid } from "./Client/ChunkGrid";

export class BattleBody {
    private battle: IBattle;
    private hubClient: IHubClient;

    private canvasContainer: HTMLDivElement;
    private canvas: HTMLCanvasElement;
    private gameCanvas: IGameCanvas;
    private camera: ICamera;
    private renderEngine: IRenderEngine;
    private chunkGrid: IChunkGrid;

    constructor(canvasContainer: HTMLDivElement, battle: IBattle, hubClient: IHubClient, width: number, height: number) {
        this.canvasContainer = canvasContainer;
        this.canvasContainer.setAttribute("style", "margin:0;font-size:0;");

        this.battle = battle;
        this.hubClient = hubClient;
       
        this.canvas = document.createElement('canvas');
        this.canvas.setAttribute("style", "image-rendering:pixelated;");
        this.canvasContainer.appendChild<HTMLCanvasElement>(this.canvas);
        this.gameCanvas = new GameCanvas(this.canvas);
        this.camera = new Camera(this.gameCanvas, width, height);
        this.chunkGrid = new ChunkGrid(battle, hubClient);
        this.renderEngine = new RenderEngine(this.gameCanvas, this.camera, this.chunkGrid);

        this.camera.onRender = this.renderEngine.render;
        this.camera.onClick = this.onClick;
        this.gameCanvas.onRender = this.renderEngine.render;
        this.chunkGrid.onUpdated = this.renderEngine.requestRender;

        window.customScale = (scale: number) => {
            this.camera.scale = scale;
        };
    }

    public resize = (width: number, height: number) : void => {
        this.gameCanvas.resize(width, height)
    }

    public onClick = (ev: PositionEvent): void => {
        let chunkXIndex = Math.floor(ev.x / (this.battle.settings.chunkWidth * this.camera.scale));
        let chunkYIndex = Math.floor(ev.y / (this.battle.settings.chunkHeight * this.camera.scale));
        let x = Math.floor(ev.x / this.camera.scale) % this.battle.settings.chunkWidth;
        x = x < 0 ? x + this.battle.settings.chunkWidth : x;
        let y = Math.floor(ev.y / this.camera.scale) % this.battle.settings.chunkHeight;
        y = y < 0 ? y + this.battle.settings.chunkHeight : y;
        this.hubClient.enqueueAction({ x: chunkXIndex, y: chunkYIndex }, { x: x, y: y, color: 0 });
        console.log(`onClick: ev:${x}:${y} chunk:${chunkXIndex}:${chunkYIndex}`);
    }
}