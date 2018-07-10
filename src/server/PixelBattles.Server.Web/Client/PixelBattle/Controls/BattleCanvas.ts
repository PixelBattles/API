import { BattleChunk } from "./BattleChunk";

export class BattleCanvas {
    private canvasContainer: HTMLDivElement;
    private canvas: HTMLCanvasElement;
    private ctx: CanvasRenderingContext2D;

    private cameraX: number;
    private cameraY: number;
    private scale: number;

    private mouseX: number;
    private mouseY: number;
    private cameraOffsetX: number = 0;
    private cameraOffsetY: number = 0;
    private isDrag: boolean;

    constructor(canvasContainer: HTMLDivElement) {
        this.canvasContainer = canvasContainer;
        this.canvasContainer.setAttribute("style", "margin:0;font-size:0;");

        this.canvas = document.createElement('canvas');
        this.canvas.setAttribute("style", "image-rendering:pixelated;");
        this.ctx = this.canvas.getContext("2d");
        this.canvasContainer.appendChild<HTMLCanvasElement>(this.canvas);

        //moving
        this.canvas.onmousedown = this.mouseDown.bind(this);
        this.canvas.onmouseup = this.mouseUp.bind(this);
        this.canvas.onmousemove = this.mouseMove.bind(this);

        this.cameraX = 0;
        this.cameraY = -50;
        this.scale = 1;
    }

    private mouseMove(ev: MouseEvent): void {
        if (this.isDrag) {
            this.cameraOffsetX = ev.clientX - this.mouseX;
            this.cameraOffsetY = ev.clientY - this.mouseY;
            this.render();
        }
    }

    private mouseDown(ev: MouseEvent): void {
        this.mouseX = ev.clientX;
        this.mouseY = ev.clientY;
        this.isDrag = true;
        this.render();
    }

    private mouseUp(ev: MouseEvent): void {
        this.isDrag = false;
        this.cameraX = this.cameraX - this.cameraOffsetX;
        this.cameraY = this.cameraY + this.cameraOffsetY;
        this.cameraOffsetX = 0;
        this.cameraOffsetY = 0;
        this.render();
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
            viewPortHalfWidth + chunk.xIndex * 100 - this.cameraX + this.cameraOffsetX,
            viewPortHalfHeight + chunk.yIndex * 100 + this.cameraY + this.cameraOffsetY);
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