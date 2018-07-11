import { IGameCanvas } from "./GameCanvas";
import { Chunk } from "./Chunk";
import { ICamera } from "./Camera";

export class RenderEngine implements IRenderEngine {
    private gameCanvas: IGameCanvas;
    private camera: ICamera;

    public constructor(canvas: IGameCanvas, camera: ICamera) {
        this.gameCanvas = canvas;
        this.camera = camera;   
    }

    public render = (): void => {
        this.renderBackground();
        let viewPortHalfHeight = this.gameCanvas.canvas.height / 2;
        let viewPortHalfWidth = this.gameCanvas.canvas.width / 2;

        let chunk1 = new Chunk("red", 0, 0);//0,0
        let chunk2 = new Chunk("green", -1, 0);//-1,0

        this.renderChunk(chunk1, viewPortHalfWidth, viewPortHalfHeight);
        this.renderChunk(chunk2, viewPortHalfWidth, viewPortHalfHeight);
    }

    private renderChunk(chunk: Chunk, viewPortHalfWidth: number, viewPortHalfHeight: number): void {
        this.gameCanvas.ctx.drawImage(
            chunk.canvas,
            viewPortHalfWidth + chunk.xIndex * 100 + this.camera.cameraX,
            viewPortHalfHeight + chunk.yIndex * 100 + this.camera.cameraY);
    }

    private renderBackground(): void {
        this.gameCanvas.ctx.beginPath();
        this.gameCanvas.ctx.rect(0, 0, this.gameCanvas.canvas.width, this.gameCanvas.canvas.height);
        this.gameCanvas.ctx.fillStyle = "grey";
        this.gameCanvas.ctx.fill();
    }
}

export interface IRenderEngine {
    render(): void;
}
