const ROWS = 10;
const COLS = 9;

function ChessGame(){

    let self = {};
    const GAME = {
        '00': Xe(0,0,0,self), '10': Ma(1,0,0,self), '20': Tinh(2,0,0,self),'30': Sy(3,0,0,self),'40': Tuong(4,0,0,self),'50': Sy(5,0,0,self),'60': Tinh(6,0,0,self),'70': Ma(7,0,0,self),'80': Xe(8,0,0,self),
        '12': Phao(1,2,0,self), '72': Phao(7,2,0,self),
        '03': Tot(0,3,0,self),'23': Tot(2,3,0,self),'43': Tot(4,3,0,self),'63': Tot(6,3,0,self),'83': Tot(8,3,0,self),
        '06': Tot(0,6,1,self),'26': Tot(2,6,1,self),'46': Tot(4,6,1,self),'66': Tot(6,6,1,self),'86': Tot(8,6,1,self),
        '17': Phao(1,7,1,self), '77': Phao(7,7,1,self),
        '09': Xe(0,9,1,self),'19': Ma(1,9,1,self),'29': Tinh(2,9,1,self),'39': Sy(3,9,1,self),'49': Tuong(4,9,1,self),'59': Sy(5,9,1,self),'69': Tinh(6,9,1,self),'79': Ma(7,9,1,self),'89': Xe(8,9,1,self),
    };

    let items;
    let histories = [];
    let _type = 1;
    
    function restart(){
        items = Object.assign(GAME);
        self.tuong = [items[40], items[49]];
    }

    self.moves = function(square, isTarget = false){
        if(square)
            if(item = items[square]){
                if(item.type == _type){
                    return item.roads();
                }
                return null;
            }
            else return null;

        let moves = {};

        for(let i in items){
            if(items[i].type === _type){
                let roads = items[i].roads[isTarget];
                moves = {...moves, ...roads}
            
            }
        }
        return moves;
    }

    self.makeMove = function(sourceSquare, targetSquare, isHistory = false){
        let item = items[sourceSquare];
        if(!item) return;
        let roads = item.roads();
        if(roads[targetSquare]){
            self.move(sourceSquare, targetSquare, isHistory);
            // let moves = self.moves(null, true);
            // if(!Object.keys(moves).length)
            //     alert(_type);
        }
        
    }
    self.move = function(sourceSquare, targetSquare, isHistory = false){
        let item = items[sourceSquare];
        if(!item) return;

        let x = targetSquare[0];
        let y = targetSquare[1];

        item.go(x,y)
        if(!isHistory)
            histories.push({sourceSquare,targetSquare, item: items[targetSquare]});
        items[targetSquare] = item;
        delete items[sourceSquare];
        _type = _type ^ 1;
    }

    self.undo = function(){
        if(!histories.length)
            return;
        let {sourceSquare,targetSquare,item} = histories.slice(-1)[0];
        self.move(targetSquare, sourceSquare,true)
        if(item)
            items[targetSquare] = item;
        else
            delete items[targetSquare];

        histories.pop();
    }

    self.start = function(){
        restart();
    }
    self.pause = function(){

    }
    self.end = function(){

    }
    function init(){
        restart();
        Object.defineProperties(self,{
            items:{
                get: function(){return items},
                set: function(value){items = value}
        }
        });

    }init();

    return self;
}