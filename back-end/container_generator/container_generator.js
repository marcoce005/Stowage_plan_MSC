const fs = require('fs');

const n_container = 6;
const container_dimension = 20;

let l = Array(n_container).fill().map((_, i) => {
    return {
        id: i, 
        discharge: Math.floor(Math.random() * 10),
        weight: Math.floor(Math.random() * 100) + 1,
        dimension: container_dimension
    }
})

fs.writeFileSync('./container_list.json', JSON.stringify(l, null, 4));