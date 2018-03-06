declare var interact: any;

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

        let canvasElement = document.createElement('canvas');
        canvasElement.height = 1000;
        canvasElement.width = 1000;
        this.canvasContainer.appendChild<HTMLCanvasElement>(canvasElement);

        this.canvas = canvasElement;
        this.width = this.canvas.width;
        this.height = this.canvas.height;
        this.ctx = this.canvas.getContext("2d");
        
        this.fillBlack(this.ctx);
        this.init();
    }

    public drawPixel(x: number, y: number, color: string): void {
        return;
    }

    public drawImage(image: HTMLImageElement): void {
        this.ctx.drawImage(image, 0, 0);
    }

    private init(): void {
        this.x = 0;
        this.y = 0;
        this.scale = 1;

        let handleMove = function (event: any) {
            this.x = this.x + event.dx;
            this.y = this.y + event.dy;
            if (event.ds) {
                this.scale = this.scale * (1 + event.ds);
            }
            
            this.canvas.style.transform = 'translate(' + this.x + 'px, ' + this.y + 'px) scale(' + this.scale + ')';
        }.bind(this);

        interact(this.canvas).draggable({
            inertia: true,
            onmove: handleMove
        }).gesturable({
            onmove: function (evt: any) {
                handleMove(evt);
            }.bind(this)
        });
    }

    private updateView() {

    }
    
    private fillBlack(ctx: CanvasRenderingContext2D): void {
        ctx.beginPath();
        ctx.rect(0, 0, 1000, 1000);
        ctx.fillStyle = "black";
        ctx.fill();
    }
}