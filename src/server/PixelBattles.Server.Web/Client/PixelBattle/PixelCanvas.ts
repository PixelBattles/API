export class PixelCanvas {
    private canvasContainer: HTMLDivElement;

    private canvas: HTMLCanvasElement;
    private ctx: CanvasRenderingContext2D;
    private width: number;
    private height: number;

    private x: number;
    private y: number;
    private scale: number;

    constructor(canvasContainer: HTMLDivElement) {
        this.canvasContainer = canvasContainer;

        this.canvas = document.createElement('canvas');
        this.canvas.height = 1000;
        this.canvas.width = 1000;
        this.ctx = this.canvas.getContext("2d");
        this.canvasContainer.appendChild<HTMLCanvasElement>(this.canvas);

        
        
        this.width = this.canvas.width;
        this.height = this.canvas.height;
        this.ctx = this.canvas.getContext("2d");
        
        this.init();
    }
    
    private init(): void {
        this.x = 0;
        this.y = 0;
        this.scale = 1;

        window.addEventListener("resize", this.onResize.bind(this));
        this.onResize();
    }
        
    private render(): void {
        this.ctx.beginPath();
        this.ctx.rect(0, 0, this.canvas.width, this.canvas.height);
        this.ctx.fillStyle = "black";
        this.ctx.fill();
    }

    private onResize(): void {
        this.canvas.width = this.canvasContainer.clientWidth;
        this.canvas.height = this.canvasContainer.clientWidth;
        this.render();
    }
}