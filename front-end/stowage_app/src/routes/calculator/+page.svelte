<script>
    import DragDrop from '$lib/components/drag_and_drop.svelte';

    let x = 0,
        y = 0,
        z = 0,
        id = 0,
        priority = 0,
        weight = 0,
        dimension = 0,
        error = false,
        different_id = false,
        container_list = Array(),
        post_ans = "";

    const add_container = () => {
        error =
            x * y * z * dimension * weight > 0 &&
            id != undefined &&
            priority != undefined
                ? false
                : true;
        different_id = container_list.map((e) => e.id).includes(id)
            ? true
            : false;
        if (error || container_list.length === x * y * z || different_id)
            return;

        container_list.push({
            id: id,
            discharge: priority,
            weight: weight,
            dimension: dimension,
        });
        container_list = [...container_list];
    };

    async function post_stowage_plan() {
        if (container_list.length === 0 || container_list.length !== x * y * z)
            return;
        const request = await fetch(
            "http://localhost:5000/api/container/GetStowagePlan?x=" +
                x +
                "&y=" +
                y +
                "&z=" +
                z,
            {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(container_list), // body data type must match "Content-Type" header
                mode: "cors", // no-cors, *cors, same-origin
            },
        );
        post_ans = await request.text();
        console.log(post_ans.split("Layer"));
    }
</script>

<svelte:head>
    <title>Stowage-plan-calculator</title>
</svelte:head>

<body>
    <h1>Insert containers</h1>

    <div class="father">
        <div class="container 1">
            <h3>Insert information abot the ship</h3>
            <form id="form1">
                <label for="x">Width: </label>
                <input type="number" min="1" max="100" bind:value={x} /><br />
                <label for="y">Length: </label>
                <input type="number" min="1" max="100" bind:value={y} /><br />
                <label for="z">Floors: </label>
                <input type="number" min="1" max="100" bind:value={z} />
            </form>
        </div>

        <div class="container 2">
            <form id="form2">
                <h3>Container information:</h3>
                <label for="scarico"
                    >ID [all the IDs have to be different]:
                </label>
                <input type="number" min="0" bind:value={id} /><br />
                <label for="scarico"
                    >Dischage priority [higher is before it will be discharged]:</label
                >
                <input
                    type="number"
                    min="0"
                    max="100"
                    bind:value={priority}
                /><br />
                <label for="peso"
                    >Weight (tons) [between 10 and 100 tons]:
                </label>
                <input
                    type="number"
                    id="peso"
                    name="y"
                    min="10"
                    max="100"
                    bind:value={weight}
                /><br />
                <label for="size"
                    >Dimension (m) [between 20 and 40 meters]:
                </label>
                <input
                    type="number"
                    id="size"
                    name="z"
                    min="20"
                    max="40"
                    bind:value={dimension}
                />
            </form>

            <button class="idpulsante" on:click={add_container}
                >AGGIUNGI CONTAINER</button
            >
        </div>

        <button type="button" class="idpulsante" on:click={post_stowage_plan}
            >STOWAGE PLAN</button
        >
    </div>

    {#if error}
        <p class="error">ERROR all input have to be greater than zero!!!</p>
    {/if}
    {#if different_id}
        <p class="error">ERROR this container already exist [change id]</p>
    {/if}

    

    <ul class="list">
        {#each container_list as e}
            <li>
                id: {e.id}, discharge: {e.discharge}, weight: {e.weight},
                dimension: {e.dimension}
            </li>
        {/each}
    </ul>

    <pre>{post_ans}</pre>
</body>

<style>
    .list {
        margin-left: 3%;
    }

    .error {
        color: red;
        padding-left: 5%;
    }

    h1 {
        text-align: center;
        font-size: 3rem;
        color: red;
    }

    .father {
        display: flex;
        margin-top: 2%;
    }

    .container {
        margin-left: 5%;
        border: 1px solid black;
        width: 30%;
        padding-left: 1%;
        display: inline-grid;
    }

    #form1 {
        padding: 4%;
        display: grid;
    }

    #form2 {
        padding: 4%;
        display: grid;
    }

    input {
        width: 15%;
    }

    .idpulsante {
        justify-self: end;
        color: darkred;
        font-size: 1.5vw;
        padding: 4%;
        background-color: bisque;
        margin: 3%;
        max-height: 150px;
    }

    .idpulsante:hover {
        cursor: pointer;
    }
</style>
