import { IGameCanvas, PositionEvent, ZoomEvent } from "./GameCanvas";

export class Camera implements ICamera {
    public onRender: () => void;
    public onClick: (ev: PositionEvent) => void;

    private canvas: IGameCanvas;

    private initialMouseX: number;
    private initialMouseY: number;
    private internalCameraX: number;
    private internalCameraY: number;
    private cameraOffsetX: number = 0;
    private cameraOffsetY: number = 0;
    private internalScale: number = 8;
    private isDrag: boolean;

    public get cameraX(): number {
        return this.internalCameraX + this.cameraOffsetX;
    }
    public get cameraY(): number {
        return this.internalCameraY + this.cameraOffsetY;
    }

    public set scale(value: number) {
        let ratio = value / this.internalScale;
        this.internalCameraX = this.internalCameraX * ratio + (this.canvas.canvas.width * ratio - this.canvas.canvas.width) / 2;
        this.internalCameraY = this.internalCameraY * ratio + (this.canvas.canvas.height * ratio - this.canvas.canvas.height) / 2;
        this.internalScale = value;
        this.onRender();
    }

    public get scale(): number {
        return this.internalScale;
    }

    public constructor(canvas: IGameCanvas, width: number, height: number) {
        this.canvas = canvas;
        this.canvas.onMoveEnd = this.onMoveEnd;
        this.canvas.onMoveStart = this.onMoveStart;
        this.canvas.onMove = this.onMove;
        this.canvas.onZoom = this.onZoom;

        this.internalCameraX = 0/*center*/ - Math.floor(width / 2);
        this.internalCameraY = 0/*center*/ - Math.floor(height / 2);
    }

    private onMove = (ev: PositionEvent): void => {
        if (!this.isDrag) {
            return;
        }

        this.cameraOffsetX = this.initialMouseX - ev.x;
        this.cameraOffsetY = this.initialMouseY - ev.y;
        this.onRender();
    }

    private onZoom = (ev: ZoomEvent): void => {
        if (ev.ratio < 0 && this.scale < 32) {
            this.scale = this.scale << 1
        }
        if (ev.ratio > 0 && this.scale > 1) {
            this.scale = this.scale >> 1;
        }
    }

    private onMoveStart = (ev: PositionEvent): void => {
        this.isDrag = true;
        this.initialMouseX = ev.x;
        this.initialMouseY = ev.y;
        this.cameraOffsetX = 0;
        this.cameraOffsetY = 0;
    }

    private onMoveEnd = (ev: PositionEvent): void => {
        if (!this.isDrag) {
            return;
        }

        this.isDrag = false;
        this.cameraOffsetX = this.initialMouseX - ev.x;
        this.cameraOffsetY = this.initialMouseY - ev.y;
        if (this.cameraOffsetX == 0 && this.cameraOffsetY == 0) {
            let position = new PositionEvent(this.internalCameraX + ev.x, this.internalCameraY + ev.y);
            this.onClick(position);
        } else {
            this.internalCameraX = this.internalCameraX + this.cameraOffsetX;
            this.internalCameraY = this.internalCameraY + this.cameraOffsetY;
            this.cameraOffsetX = 0;
            this.cameraOffsetY = 0;
        }
        this.onRender();
    }
}

export interface ICamera {
    onRender: () => void;
    onClick: (ev: PositionEvent) => void;

    cameraX: number;
    cameraY: number;
    scale: number;
}
