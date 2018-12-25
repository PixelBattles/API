import { Chunk } from "./Chunk";

export class ChunkGrid implements IChunkGrid {
    private storage: { [key: number]: Chunk; } = {};
    public defaultWidth: number;
    public defaultHeight: number;

    public constructor(defaultWidth: number, defaultHeight: number) {
        this.defaultWidth = defaultWidth;
        this.defaultHeight = defaultHeight;
    }

    public getChunks(xIndexFrom: number, xIndexTo: number, yIndexFrom: number, yIndexTo: number): Chunk[] {
        return [new Chunk("red", 0, 0), new Chunk("green", -1, 0)];
    }
}
export interface IChunkGrid {
    getChunks(xIndexFrom: number, xIndexTo: number, yIndexFrom: number, yIndexTo: number): Chunk[];
    defaultWidth: number;
    defaultHeight: number;
}