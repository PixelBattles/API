import { BattleChunk } from "./BattleChunk";

export class BattleCanvas {
    private canvasContainer: HTMLDivElement;
    private canvas: HTMLCanvasElement;
    private ctx: CanvasRenderingContext2D;

    private centerX: number;
    private centerY: number;
    private scale: number;

    constructor(canvasContainer: HTMLDivElement) {
        this.canvasContainer = canvasContainer;
        this.canvasContainer.setAttribute("style", "margin:0;font-size:0;");

        this.canvas = document.createElement('canvas');
        this.canvas.setAttribute("style", "image-rendering:pixelated;");
        this.ctx = this.canvas.getContext("2d");
        this.canvasContainer.appendChild<HTMLCanvasElement>(this.canvas);

        this.centerX = 0;
        this.centerY = -50;
        this.scale = 1;
    }
        
    private render(): void {
        this.renderBackground();
        let viewPortHalfHeight = this.canvas.height / 2;
        let viewPortHalfWidth = this.canvas.width / 2;

        let chunk1 = new BattleChunk("red", 0, 0);//0,0
        let chunk2 = new BattleChunk("green", -1, 0);//-1,0

        this.renderChunk(chunk1, viewPortHalfWidth, viewPortHalfHeight);
        this.renderChunk(chunk2, viewPortHalfWidth, viewPortHalfHeight);
    }

    private renderChunk(chunk: BattleChunk, viewPortHalfWidth: number, viewPortHalfHeight: number): void {
        this.ctx.drawImage(
            chunk.canvas,
            viewPortHalfWidth + chunk.xIndex * 100 - this.centerX,
            viewPortHalfHeight + chunk.yIndex * 100 + this.centerY);
    }

    private renderBackground(): void {
        this.ctx.beginPath();
        this.ctx.rect(0, 0, this.canvas.width, this.canvas.height);
        this.ctx.fillStyle = "grey";
        this.ctx.fill();
    }

    public resize(width: number, height: number) {
        this.canvas.width = width;
        this.canvas.height = height;
        this.render();
    }
}