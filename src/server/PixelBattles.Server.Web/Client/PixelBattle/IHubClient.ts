export interface IHubClient {
    connectToGame(gameId: string): Promise<boolean>;
    disconnectFromGame(gameId: string): Promise<boolean>;
    getGameState(gameId: string): Promise<IGameState>;
    processAction(action: IUserAction): Promise<IActionResult>;
    getGameDelta(gameId: string, from: number, to: number): Promise<IDeltaResult>;
}

export interface IPendingAction extends IUserAction{
    changeIndex: number;
}

export interface IUserAction {
    xIndex: number;
    yIndex: number;
    color: string;
}

export interface IGameState {
    changeIndex: number;
    state: string;
    pendingActions: { [key: number]: IPendingAction };
}

export interface IActionResult extends IResult {
    userAction: IPendingAction;
}

export interface IDeltaResult extends IResult {
    userActions: IPendingAction[];
}

export interface IResult {
    succeeded: boolean;
    errors: IError[];
}

export interface IError {
    code: string;
    description: string;
}