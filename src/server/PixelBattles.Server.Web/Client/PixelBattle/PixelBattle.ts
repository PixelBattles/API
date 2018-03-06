import { PixelGame } from "./PixelGame";

export class PixelBattle {
    private battleId: string;
    private battleContainer: HTMLDivElement;

    private game: PixelGame;
    private gameContainer: HTMLDivElement;
    
    constructor(battleContainer: HTMLDivElement, battleId: string) {
        this.battleContainer = battleContainer;
        this.battleId = battleId;

        this.gameContainer = this.createGameContainer();
        this.battleContainer.appendChild(this.gameContainer);
        this.game = this.createGame(this.gameContainer, this.battleId);
    }

    private createGameContainer(): HTMLDivElement {
        let gameContainer: HTMLDivElement = <HTMLDivElement>document.createElement('div');
        gameContainer.className = "gameContainer";
        return gameContainer;
    }

    private createGame(gameContainer: HTMLDivElement, battleId : string): PixelGame {
        let game: PixelGame = new PixelGame(gameContainer, battleId);
        return game;
    }
}