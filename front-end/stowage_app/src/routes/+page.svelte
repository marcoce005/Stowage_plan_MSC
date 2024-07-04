<script>
</script>

<svelte:head>
    <title>Stowage-plan</title>
</svelte:head>

<body>
    <h1>STOWAGE PLAN</h1>

    <div class="container">
        <a href="/calculator">Stowage plan calculator</a>

        <a href="/container-generator">Container generator</a>
    </div>
</body>

<style>
    h1 {
        text-align: center;
        font-size: 3rem;
    }

    .container {
        margin-top: 10%;
        display: flex;
        flex-direction: row;
        justify-content: center;
        column-gap: 15%;
    }

    a {
        text-decoration: none;
        text-align: center;
        font-size: 2rem;
        padding: 5%;
        border: 3px solid blue;
        border-radius: 7px;
        background-color: rgba(0, 0, 255, 0.274);
    }

    a:hover {
        background-color: rgba(0, 0, 0, 0.2);
        color: black;
        border-color: black;
    }
</style>
