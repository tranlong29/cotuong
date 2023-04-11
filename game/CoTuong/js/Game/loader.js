const IMAGES_DIR = '../images';

class Loader{
    constructor(){
        let srcs = {
            dot:    'dot',
            selected:'selected',
            table:  'Board',
            xe_r:   'xe_r',
            phao_r: 'phao_r',
            ma_r:   'ma_r',
            tuong_r:'tuong_r',
            sy_r:   'sy_r',
            tinh_r: 'tinh_r',
            tot_r:  'tot_r',
            xe_b:   'xe_b',
            phao_b: 'phao_b',
            ma_b:   'ma_b',
            tuong_b:'tuong_b',
            sy_b:   'sy_b',
            tinh_b: 'tinh_b',
            tot_b:  'tot_b',
        }

        this.loadding = 0;


        for(let i in srcs){
            let name = srcs[i];
            this[i] = new Image;
            this[i].src = `${IMAGES_DIR}/${name}.png`;
            this.loadding++;
            this[i].onload = () =>{
                this.loadding--;
                if(this.loadding == 0)
                    new chessgame;
            }
        }

    }
}
let loader = new Loader;