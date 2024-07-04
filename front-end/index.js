let cont = 0;
let list_container = [];

function add_container() {
    let x = document.getElementById("x").value;
    let y = document.getElementById("y").value;
    let z = document.getElementById("z").value;
    if (x*y*z == 0 || cont >= x*y*z)
        window.alert("ROTTO IN CULO");
    else {
        cont++;
        let list = document.getElementById("nave");
        let scarico = document.getElementById("scarico").value;
        let peso = document.getElementById("peso").value;
        let size = document.getElementById("size").value;
        if (scarico != "" && peso != "" && size != "") {
            let li = document.createElement("li");
            list_container.push({ discharge : parseInt(scarico), weight : parseFloat(peso), dimension : parseFloat(size) });
            li.innerText = "priorità di scarico: " + scarico + " peso: " + peso + " dimensione: " + size;
            list.append(li);
        } else
            window.alert("ROTTO IN CULO");
    }
}

function stowage_plan() {
    let JsonString = JSON.stringify(list_container);
    console.log(JsonString);

    let url = new URL("http://localhost:5000/api/container/GetStowagePlan");
    const params = { x: document.getElementById("x").value, y: document.getElementById("y").value, z: document.getElementById("z").value};
    Object.keys(params).forEach(key => url.searchParams.append(key, params[key]));
    const options = {
        method: "POST",
        headers: {
             "Content-Type": "application/json"
        },
        body: JsonString,
        mode: "no-cors"
    };
    let request = new Request(url, options);

    fetch(request)
    .then(response => {
        console.log(response);
    })
    .catch(error => console.error(error));
}