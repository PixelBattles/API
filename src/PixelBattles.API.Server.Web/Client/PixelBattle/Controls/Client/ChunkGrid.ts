import { Chunk } from "./Chunk";
import { IBattle } from "../../Clients/IApiClient";
import { IHubClient } from "../../Clients/IHubClient";

export class ChunkGrid implements IChunkGrid {
    private storage: Chunk[][];
    private battle: IBattle;
    private hubClient: IHubClient;
    public defaultWidth: number;
    public defaultHeight: number;

    public constructor(battle: IBattle, hubClient: IHubClient) {
        this.battle = battle;
        this.hubClient = hubClient;
        this.defaultWidth = this.battle.settings.chunkWidth;
        this.defaultHeight = this.battle.settings.chunkHeight;
        this.storage = new Array<Array<Chunk>>();
    }

    public getChunks(xIndexFrom: number, xIndexTo: number, yIndexFrom: number, yIndexTo: number): Chunk[] {
        let resultChunks = new Array<Chunk>();
        for (var x = Math.max(this.battle.settings.minWidthIndex, xIndexFrom); x <= Math.min(this.battle.settings.maxWidthIndex, xIndexTo); x++) {
            for (var y = Math.max(this.battle.settings.minHeightIndex, yIndexFrom); y <= Math.min(this.battle.settings.maxHeightIndex, yIndexTo); y++) {
                let chunk: Chunk;
                if (this.storage[x]) {
                    chunk = this.storage[x][y];
                }
                else {
                    this.storage[x] = new Array<Chunk>();
                }
                if (chunk) {
                    resultChunks.push(chunk);
                }
                else {
                    resultChunks.push(this.storage[x][y] = new Chunk(this.hubClient, x, y, this.battle.settings.chunkWidth, this.battle.settings.chunkHeight));
                }
            }
        }
        return resultChunks;
    }
}
export interface IChunkGrid {
    getChunks(xIndexFrom: number, xIndexTo: number, yIndexFrom: number, yIndexTo: number): Chunk[];
    defaultWidth: number;
    defaultHeight: number;
}