import { IGameCanvas } from "./GameCanvas";
import { Chunk } from "./Chunk";
import { ICamera } from "./Camera";
import { IChunkGrid } from "./ChunkGrid";

export class RenderEngine implements IRenderEngine {
    private gameCanvas: IGameCanvas;
    private camera: ICamera;
    private chunkGrid: IChunkGrid;
    private isRenderRequired: boolean;

    public constructor(canvas: IGameCanvas, camera: ICamera, chunkGrid: IChunkGrid) {
        this.gameCanvas = canvas;
        this.camera = camera;   
        this.chunkGrid = chunkGrid;
        this.isRenderRequired = false;
    }

    public render = (): void => {
        this.isRenderRequired = false;

        this.renderBackground();
        let chunkWidth = this.chunkGrid.chunkWidth * this.camera.scale;
        let chunkHeight = this.chunkGrid.chunkHeight * this.camera.scale;
        for (let chunk of this.getVisibleChunks(chunkWidth, chunkHeight)) {
            this.renderChunk(chunk, chunkWidth, chunkHeight);
        }
    }

    public requestRender = (): void => {
        if (!this.isRenderRequired) {
            this.isRenderRequired = true;
            setTimeout((() => { if (this.isRenderRequired) { this.render(); } }).bind(this), 500);
        }
    }

    private renderChunk(chunk: Chunk, chunkWidth: number, chunkHeight: number): void {
        this.gameCanvas.ctx.drawImage(
            chunk.canvas,
            chunk.xIndex * chunkWidth - this.camera.cameraX,
            chunk.yIndex * chunkHeight - this.camera.cameraY,
            chunkWidth,
            chunkHeight);
    }

    private getVisibleChunks(chunkWidth: number, chunkHeight: number): Chunk[] {
        let minX = Math.floor(this.camera.cameraX / chunkWidth);
        let maxX = Math.ceil((this.camera.cameraX + this.gameCanvas.canvas.width) / chunkWidth);
        let minY = Math.floor(this.camera.cameraY / chunkHeight);
        let maxY = Math.ceil((this.camera.cameraY + this.gameCanvas.canvas.height) / chunkHeight);
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
    requestRender(): void;
}
