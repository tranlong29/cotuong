const R_LEFT = 3;
const R_RIGHT = 5;
const R_TOP_B = 2;
const R_TOP_R = 7;
const S_TOP = 4;
const S_BOT = 5;

function ChessItem(x, y, name, type, game) {
  let self = { x, y, name, type };
  self._roads = function () {};

  function kt_mat_tuong() {
    let { x, y, name } = game.tuong[0];
    let { x: _x, y: _y } = game.tuong[1];

    for (let i = y + 1; i <= _y; i++) {
      let item = game.items[`${x}${i}`] || {};
      if (item.name)
        if (item.name != name) return false;
        else return true;
    }
    return false;
  }

  self.roads = function (isTarget = true) {
    let roads = self._roads();
    let { x, y } = self;

    for (let i in roads) {
      game.move(`${x}${y}`, i);

      if (kt_mat_tuong()) {
        delete roads[i];
      } 
      else if (isTarget) {
        let moves = game.moves();
        let { x, y } = game.tuong[self.type];
        if (moves[`${x}${y}`]) delete roads[i];
      }
      game.undo();
    }
    return roads;
  };

  self.check = function (x, y) {
    let item = game.items[`${x}${y}`] || {};
    return !(
      item.type == self.type ||
      x < 0 ||
      x >= COLS ||
      y < 0 ||
      y >= ROWS
    );
  };

  self.go = function (x, y) {
    self.x = Number(x);
    self.y = Number(y);
  };

  function init() {}
  init();

  return self;
}
function Xe(x, y, type = 0, game) {
  let self = ChessItem(x, y, 'xe', type, game);

  self._roads = function () {
    let { x, y } = self;
    let roads = {};

    for (let i = x + 1; i < COLS; i++) {
      if (self.check(i, y)) roads[`${i}${y}`] = true;
      if (game.items[`${i}${y}`]) break;
    }

    for (let i = x - 1; i >= 0; i--) {
      if (self.check(i, y)) roads[`${i}${y}`] = true;
      if (game.items[`${i}${y}`]) break;
    }

    for (let i = y + 1; i < ROWS; i++) {
      if (self.check(x, i)) roads[`${x}${i}`] = true;
      if (game.items[`${x}${i}`]) break;
    }

    for (let i = y - 1; i >= 0; i--) {
      if (self.check(x, i)) roads[`${x}${i}`] = true;
      if (game.items[`${x}${i}`]) break;
    }

    return roads;
  };
  function init() {}
  init();

  return self;
}
function Phao(x, y, type = 0, game) {
  let self = ChessItem(x, y, "phao", type, game);
  self._roads = function () {
    let { x, y } = self;

    let roads = {};

    let flag = 0;

    for (let i = x + 1; i < COLS; i++) {
      if ((item = game.items[`${i}${y}`])) {
        if (flag && item) {
          if (item.type != self.type && self.check(i, y)) roads[`${i}${y}`] = true;
          break;
        }
        flag = 1;
      }
      if (!flag && self.check(i, y)) {
        roads[`${i}${y}`] = true;
      }
    }
    flag = 0;
    for (let i = x - 1; i >= 0; i--) {
      if ((item = game.items[`${i}${y}`])) {
        if (flag && item) {
          if (item.type != self.type && self.check(i, y)) roads[`${i}${y}`] = true;
          break;
        }
        flag = 1;
      }
      if (!flag && self.check(i, y)) {
        roads[`${i}${y}`] = true;
      }
    }
    flag = 0;
    for (let i = y + 1; i < ROWS; i++) {
      if ((item = game.items[`${x}${i}`])) {
        if (flag && item) {
          if (item.type != self.type && self.check(x, i)) roads[`${x}${i}`] = true;
          break;
        }
        flag = 1;
      }
      if (!flag && self.check(x, i)) {
        roads[`${x}${i}`] = true;
      }
    }
    flag = 0;
    for (let i = y - 1; i >= 0; i--) {
      if ((item = game.items[`${x}${i}`])) {
        if (flag && item) {
          if (item.type != self.type && self.check(x, i)) roads[`${x}${i}`] = true;
          break;
        }
        flag = 1;
      }
      if (!flag && self.check(x, i)) {
        roads[`${x}${i}`] = true;
      }
    }
    return roads;
  };
  function init() {}
  init();

  return self;
}
function Ma(x, y, type = 0, game) {
  let self = ChessItem(x, y, "ma", type, game);
  self._roads = function () {
    let { x, y } = self;
    let roads = {};
    if (!game.items[`${x + 1}${y}`]) {
      if (self.check(x + 2, y + 1)) roads[`${x + 2}${y + 1}`] = true;
      if (self.check(x + 2, y - 1)) roads[`${x + 2}${y - 1}`] = true;
    }
    if (!game.items[`${x - 1}${y}`]) {
      if (self.check(x - 2, y + 1)) roads[`${x - 2}${y + 1}`] = true;
      if (self.check(x - 2, y - 1)) roads[`${x - 2}${y - 1}`] = true;
    }
    if (!game.items[`${x}${y + 1}`]) {
      if (self.check(x + 1, y + 2)) roads[`${x + 1}${y + 2}`] = true;
      if (self.check(x - 1, y + 2)) roads[`${x - 1}${y + 2}`] = true;
    }
    if (!game.items[`${x}${y - 1}`]) {
      if (self.check(x + 1, y - 2)) roads[`${x + 1}${y - 2}`] = true;
      if (self.check(x - 1, y - 2)) roads[`${x - 1}${y - 2}`] = true;
    }

    return roads;
  };
  function init() {}
  init();

  return self;
}
function Tuong(x, y, type = 0, game) {
  let self = ChessItem(x, y, "tuong", type, game);
  self._roads = function () {
    let { x, y } = self;
    let roads = {};
    if (x < R_RIGHT && self.check(x + 1, y)) roads[`${x + 1}${y}`] = true;
    if (x > R_LEFT && self.check(x - 1, y)) roads[`${x - 1}${y}`] = true;
    if (self.type ? y < ROWS - 1 : y < R_TOP_B && self.check(x, y + 1))
      roads[`${x}${y + 1}`] = true;
    if (self.type ? y > R_TOP_R : y > 0 && self.check(x, y - 1))
      roads[`${x}${y - 1}`] = true;

    return roads;
  };
  function init() {}
  init();

  return self;
}
function Sy(x, y, type = 0, game) {
  let self = ChessItem(x, y, "sy", type, game);
  self._roads = function () {
    let { x, y } = self;
    let roads = {};
    if (
      x < R_RIGHT &&
      (self.type ? y < ROWS - 1 : y < R_TOP_B) &&
      self.check(x + 1, y + 1)
    ) {
      roads[`${x + 1}${y + 1}`] = true;
    }
    if (
      x > R_LEFT &&
      (self.type ? y > R_TOP_R : y > 0) &&
      self.check(x - 1, y - 1)
    ) {
      roads[`${x - 1}${y - 1}`] = true;
    }
    if (
      x > R_LEFT &&
      (self.type ? y < ROWS - 1 : y < R_TOP_B) &&
      self.check(x - 1, y + 1)
    )
      roads[`${x - 1}${y + 1}`] = true;
    if (
      x < R_RIGHT &&
      (self.type ? y > R_TOP_R : y > 0) &&
      self.check(x + 1, y - 1)
    )
      roads[`${x + 1}${y - 1}`] = true;

    return roads;
  };
  function init() {}
  init();

  return self;
}
function Tinh(x, y, type = 0, game) {
  let self = ChessItem(x, y, "tinh", type, game);
  self._roads = function () {
    let { x, y } = self;
    let roads = {};
    if (!game.items[`${x + 1}${y + 1}`] && (self.type ? y < ROWS - 1 : y < S_TOP) && self.check(x + 2, y + 2))
      roads[`${x + 2}${y + 2}`] = true;
    if (!game.items[`${x - 1}${y - 1}`] &&(self.type ? y > S_BOT : y > 0) && self.check(x - 2, y - 2))
      roads[`${x - 2}${y - 2}`] = true;
    if (!game.items[`${x - 1}${y + 1}`] && (self.type ? y < ROWS - 1 : y < S_TOP) && self.check(x - 2, y + 2))
      roads[`${x - 2}${y + 2}`] = true;
    if (!game.items[`${x + 1}${y - 1}`] &&(self.type ? y > S_BOT : y > 0) &&self.check(x + 2, y - 2))
      roads[`${x + 2}${y - 2}`] = true;
    return roads;
  };
  function init() {}
  init();

  return self;
}
function Tot(x, y, type = 0, game) {
  let self = ChessItem(x, y, "tot", type, game);
  self._roads = function () {
    let { x, y } = self;
    let roads = {};

    switch (self.type) {
      case 0:
        {
          if (self.check(x, y + 1)) roads[`${x}${y + 1}`] = true;
          if (y > S_TOP) {
            if (self.check(x + 1, y)) roads[`${x + 1}${y}`] = true;
            if (self.check(x - 1, y)) roads[`${x - 1}${y}`] = true;
          }
        }
        break;
      case 1:
        {
          if (self.check(x, y - 1)) roads[`${x}${y - 1}`] = true;
          if (y < S_BOT) {
            if (self.check(x + 1, y)) roads[`${x + 1}${y}`] = true;
            if (self.check(x - 1, y)) roads[`${x - 1}${y}`] = true;
          }
        }

        break;
    }
    return roads;
  };
  function init() {}
  init();

  return self;
}
