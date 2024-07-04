let list = [
    {
        "id": 0,
        "discharge": 9,
        "weight": 29,
        "dimension": 20
    },
    {
        "id": 1,
        "discharge": 3,
        "weight": 62,
        "dimension": 20
    },
    {
        "id": 2,
        "discharge": 5,
        "weight": 92,
        "dimension": 20
    },
    {
        "id": 3,
        "discharge": 5,
        "weight": 63,
        "dimension": 20
    },
    {
        "id": 4,
        "discharge": 1,
        "weight": 9,
        "dimension": 20
    },
    {
        "id": 5,
        "discharge": 6,
        "weight": 25,
        "dimension": 20
    }
];

async function ciro() {
    const request = await fetch('http://localhost:5000/api/container/GetStowagePlan?x=3&y=2&z=1', {
        method: "POST", // *GET, POST, PUT, DELETE, etc.
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(list), // body data type must match "Content-Type" header
        mode: "cors", // no-cors, *cors, same-origin
    });

    console.log(await request.text());
};

ciro()