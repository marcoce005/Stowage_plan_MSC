<script>
    let x = 0,
        y = 0,
        z = 0,
        dimension = 0,
        error = false;

    let container_list = Array();
    const generate_json = () => {
        error = x * y * z * dimension > 0 ? false : true; // check that all input are correct
        if (error) return;

        container_list = Array(x * y * z)
            .fill(0)
            .map((_, i) => {
                return {
                    id: i,
                    discharge: Math.floor(Math.random() * 10),
                    weight: Math.floor(Math.random() * 100) + 1,
                    dimension: dimension,
                };
            });
    };

    function download() {
        if (container_list.length === 0) return;

        let dataStr =
            "data:text/json;charset=utf-8," +
            encodeURIComponent(JSON.stringify(container_list, null, 4));
        let downloadAnchorNode = document.createElement("a");
        downloadAnchorNode.setAttribute("href", dataStr);
        downloadAnchorNode.setAttribute("download", "container_list.json");
        document.body.appendChild(downloadAnchorNode); // required for firefox
        downloadAnchorNode.click();
        downloadAnchorNode.remove();
    }
</script>

<svelte:head>
    <title>Container-generator</title>
</svelte:head>

<h1>Container Generator</h1>

<p>
    This script allow you to generate a JSON file that contains a list of
    containers to test the stowage plan algorithm.<br />It require only the
    dimension of the ship and the dimension of the containers [it's suppose that
    all containers are long the same]
</p>

<div class="container">
    <div class="form">
        <div class="item">
            Width : <input type="number" min="1" max="1000" bind:value={x} />
        </div>
        <div class="item">
            Length :<input type="number" min="1" max="1000" bind:value={y} />
        </div>
        <div class="item">
            Floors : <input type="number" min="1" max="1000" bind:value={z} />
        </div>
        <div class="item">
            Dimension : <input
                type="number"
                min="1"
                max="1000"
                bind:value={dimension}
                style="margin-left: 11%"
            />
        </div>
        <button on:click={generate_json}>Submit</button>

        <button class="download" on:click={download}>Download</button>

        {#if error}
            <p class="error">ERROR all input have to be greater than zero!!!</p>
        {/if}
    </div>

    <div class="preview">
        <pre>{JSON.stringify(container_list, null, 4)}</pre>
    </div>
</div>

<style>
    .preview {
        width: 55%;
        border: 1px solid black;
        margin-right: 5%;
    }

    .container {
        display: flex;
        flex-direction: row;
        justify-content: space-between;
    }

    h1 {
        text-align: center;
        font-size: 3rem;
    }

    p {
        padding: 5%;
        padding-top: 2%;
        font-size: 1.5rem;
        text-align: justify;
    }

    .error {
        padding: 0;
        color: red;
    }

    .form {
        width: 22%;
        display: flex;
        flex-direction: column;
        row-gap: 15px;
        padding-left: 5%;
    }

    .item {
        font-size: 2rem;
    }

    .item input {
        font-size: 1.5rem;
        margin-left: 25%;
    }

    button {
        font-size: 2rem;
        width: 100%;
    }
</style>
