import { Chunk } from "./Client/Chunk";
import { IGameCanvas, GameCanvas } from "./Client/GameCanvas";
import { ICamera, Camera } from "./Client/Camera";
import { IRenderEngine, RenderEngine } from "./Client/RenderEngine";

export class BattleBody {
    private canvasContainer: HTMLDivElement;
    private canvas: HTMLCanvasElement;
    private gameCanvas: IGameCanvas;
    private camera: ICamera;
    private renderEngine: IRenderEngine;

    constructor(canvasContainer: HTMLDivElement) {
        this.canvasContainer = canvasContainer;
        this.canvasContainer.setAttribute("style", "margin:0;font-size:0;");
       
        this.canvas = document.createElement('canvas');
        this.canvas.setAttribute("style", "image-rendering:pixelated;");
        this.canvasContainer.appendChild<HTMLCanvasElement>(this.canvas);
        this.gameCanvas = new GameCanvas(this.canvas);
        this.camera = new Camera(this.gameCanvas);
        this.renderEngine = new RenderEngine(this.gameCanvas, this.camera);

        this.camera.onRender = this.renderEngine.render;
        this.gameCanvas.onRender = this.renderEngine.render;
    }

    public resize = (width: number, height: number) : void => {
        this.gameCanvas.resize(width, height)
    }
}