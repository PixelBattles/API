import { Chunk, IChunk } from "./Chunk";
import { IBattle } from "../../Clients/IApiClient";
import { IHubClient } from "../../Clients/IHubClient";

export class ChunkGrid implements IChunkGrid {
    private storage: Chunk[][];
    private battle: IBattle;
    private hubClient: IHubClient;

    public chunkWidth: number;
    public chunkHeight: number;
    public onUpdated: () => void;

    public constructor(battle: IBattle, hubClient: IHubClient) {
        this.battle = battle;
        this.hubClient = hubClient;
        this.chunkWidth = this.battle.settings.chunkWidth;
        this.chunkHeight = this.battle.settings.chunkHeight;
        this.storage = new Array<Array<Chunk>>();
    }

    public getChunks(xIndexFrom: number, xIndexTo: number, yIndexFrom: number, yIndexTo: number): Chunk[] {
        let resultChunks = new Array<Chunk>();
        for (var x = Math.max(this.battle.settings.minWidthIndex, xIndexFrom); x <= Math.min(this.battle.settings.maxWidthIndex, xIndexTo); x++) {
            for (var y = Math.max(this.battle.settings.minHeightIndex, yIndexFrom); y <= Math.min(this.battle.settings.maxHeightIndex, yIndexTo); y++) {
                resultChunks.push(this.getOrAddChunkInternal(x, y));
            }
        }
        return resultChunks;
    }
    
    public getChunk(x: number, y: number): Chunk {
        
        if (x > this.battle.settings.maxWidthIndex || x < this.battle.settings.minWidthIndex) {
            return;
        }
        if (y > this.battle.settings.maxHeightIndex || y < this.battle.settings.minHeightIndex) {
            return;
        }
        return this.getOrAddChunkInternal(x, y);
    }

    private getOrAddChunkInternal(x: number, y: number): Chunk {
        let chunk: Chunk;
        if (this.storage[x]) {
            chunk = this.storage[x][y];
        }
        else {
            this.storage[x] = new Array<Chunk>();
        }
        if (chunk) {
            return chunk;
        }
        else {
            return this.storage[x][y] = new Chunk(this.hubClient, x, y, this.battle.settings.chunkWidth, this.battle.settings.chunkHeight, this.onChunkUpdated);
        }
    }

    private onChunkUpdated = (chunk: IChunk) => this.onUpdated();
}
export interface IChunkGrid {
    getChunk(x: number, y: number): Chunk;
    getChunks(xIndexFrom: number, xIndexTo: number, yIndexFrom: number, yIndexTo: number): Chunk[];
    onUpdated: () => void;
    chunkWidth: number;
    chunkHeight: number;
}