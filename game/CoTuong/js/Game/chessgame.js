const GAME_WIDTH = 400;
const GAME_HEIGHT = 600;

const ITEM_WIDTH = GAME_WIDTH / 9;
const ITEM_HEIGHT = GAME_HEIGHT / 10;

let game = new ChessGame();

class chessgame {
  constructor() {
    let canvas = document.createElement("canvas");
    canvas.width = GAME_WIDTH;
    canvas.height = GAME_HEIGHT;

    this.ctx = canvas.getContext("2d");
    this.game = game;
    this.itemSelected;
    this.roads = {};
    this.old_pos;

    document.body.appendChild(canvas);

    this.update();

    document.getElementById('undo').onclick = ()=>{
      this.game.undo();
      this.draw();
    }


    canvas.onclick = function (e) {
      let x = e.clientX;
      let y = e.clientY;

      x = Math.floor(x / ITEM_WIDTH);
      y = Math.floor(y / ITEM_HEIGHT);

      let moves = game.moves(`${x}${y}`);
      if (moves) {
        this.old_pos = {};
        this.roads = moves;
        this.itemSelected = {x,y};
        this.draw();
        this.old_pos = {x,y};
      } else if (this.roads[`${x}${y}`]) {
        let _x = this.itemSelected.x;
        let _y = this.itemSelected.y;
        game.makeMove(`${_x}${_y}`, `${x}${y}`);
        this.roads = {};
        this.draw();
        this.itemSelected = null;
      }
    }.bind(this);
  }
  update() {
    this.draw();
  }
  draw() {
    this.ctx.clearRect(0, 0, GAME_WIDTH, GAME_HEIGHT);
    this.ctx.drawImage(loader.table, 0, 0, GAME_WIDTH, GAME_HEIGHT);

    for (let i in this.game.items) {
      let item = this.game.items[i];
      let name = `${item.name}_${item.type ? "r" : "b"}`;
      this.ctx.drawImage(
        loader[name],
        item.x * ITEM_WIDTH,
        item.y * ITEM_HEIGHT,
        ITEM_WIDTH,
        ITEM_WIDTH
      );
    }
    if (this.itemSelected)
      this.ctx.drawImage(
        loader.selected,
        this.itemSelected.x * ITEM_WIDTH,
        this.itemSelected.y * ITEM_HEIGHT,
        ITEM_WIDTH,
        ITEM_WIDTH
      );
      if (this.old_pos)
      this.ctx.drawImage(
        loader.selected,
        this.old_pos.x * ITEM_WIDTH,
        this.old_pos.y * ITEM_HEIGHT,
        ITEM_WIDTH,
        ITEM_WIDTH
      );
    if(this.roads){
        for(let i in this.roads){
            this.ctx.drawImage(loader.dot, i[0] * ITEM_WIDTH + ITEM_WIDTH/ 3.3, i[1] * ITEM_HEIGHT + ITEM_HEIGHT/3.3, ITEM_WIDTH/2, ITEM_WIDTH/2)
        }
    }
  }
}
