export interface IHubClient {
    onConnected: Promise<void>;
    subscribeToChunk(key: IChunkKey, callback: (message: IChunkStreamMessage) => void): void;
    unsubscribeFromChunk(key: IChunkKey): void;
}

export interface IChunkState {
    changeIndex: number;
    image: string;
}

export interface IChunkKey {
    x: number;
    y: number;
}

export interface IChunkAction {
    x: number;
    y: number;
    color: string;
}

export interface IChunkStreamMessage {
    state: IChunkState;
    key: IChunkKey;
    action: IChunkAction;
}