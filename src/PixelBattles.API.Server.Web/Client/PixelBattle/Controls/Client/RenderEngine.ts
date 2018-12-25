import { IGameCanvas } from "./GameCanvas";
import { Chunk } from "./Chunk";
import { ICamera } from "./Camera";
import { IChunkGrid } from "./ChunkGrid";

export class RenderEngine implements IRenderEngine {
    private gameCanvas: IGameCanvas;
    private camera: ICamera;
    private chunkGrid: IChunkGrid;

    public constructor(canvas: IGameCanvas, camera: ICamera, chunkGrid: IChunkGrid) {
        this.gameCanvas = canvas;
        this.camera = camera;   
        this.chunkGrid = chunkGrid;
    }

    public render = (): void => {
        this.renderBackground();
        let viewPortHalfHeight = Math.floor(this.gameCanvas.canvas.height / 2);
        let viewPortHalfWidth = Math.floor(this.gameCanvas.canvas.width / 2);
        for (let chunk of this.getVisibleChunks(viewPortHalfHeight, viewPortHalfWidth)) {
            this.renderChunk(chunk, viewPortHalfHeight, viewPortHalfWidth);
        }
    }

    private renderChunk(chunk: Chunk, viewPortHalfHeight: number, viewPortHalfWidth: number): void {
        this.gameCanvas.ctx.drawImage(
            chunk.canvas,
            viewPortHalfWidth + this.camera.cameraX + chunk.xIndex * (this.chunkGrid.defaultWidth * this.camera.scale),
            viewPortHalfHeight + this.camera.cameraY + chunk.yIndex * (this.chunkGrid.defaultHeight * this.camera.scale),
            (this.chunkGrid.defaultWidth * this.camera.scale),
            (this.chunkGrid.defaultHeight * this.camera.scale));
    }

    private getVisibleChunks(viewPortHalfHeight: number, viewPortHalfWidth: number): Chunk[] {
        let minX = Math.floor((- this.camera.cameraX - viewPortHalfWidth) / (this.chunkGrid.defaultWidth * this.camera.scale));
        let maxX = Math.ceil((- this.camera.cameraX + viewPortHalfWidth) / (this.chunkGrid.defaultWidth * this.camera.scale)) - 1;
        let minY = Math.floor((- this.camera.cameraY - viewPortHalfHeight) / (this.chunkGrid.defaultHeight * this.camera.scale));
        let maxY = Math.ceil((- this.camera.cameraY + viewPortHalfHeight) / (this.chunkGrid.defaultHeight * this.camera.scale)) - 1;
        return this.chunkGrid.getChunks(minX, maxX, minY, maxY);
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
