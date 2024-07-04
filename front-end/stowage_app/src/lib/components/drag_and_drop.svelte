<script>
    import * as fileSize from "filesize";
    import { filedrop } from "filedrop-svelte";
    let files;
    let options = {};
</script>

<div
    use:filedrop={options}
    on:filedrop={(e) => {
        files = e.detail.files;
    }}
    class="filedrop"
>
    <svg
        xmlns="http://www.w3.org/2000/svg"
        viewBox="0 0 24 24"
        width="48"
        height="48"
        ><path fill="none" d="M0 0h24v24H0z" /><path
            d="M1 14.5a6.496 6.496 0 0 1 3.064-5.519 8.001 8.001 0 0 1 15.872 0 6.5 6.5 0 0 1-2.936 12L7 21c-3.356-.274-6-3.078-6-6.5zm15.848 4.487a4.5 4.5 0 0 0 2.03-8.309l-.807-.503-.12-.942a6.001 6.001 0 0 0-11.903 0l-.12.942-.805.503a4.5 4.5 0 0 0 2.029 8.309l.173.013h9.35l.173-.013zM13 13v4h-2v-4H8l4-5 4 5h-3z"
        /></svg
    >
    <p>Drag &amp; drop files</p>
</div>
{#if files}
    <h2>Accepted files</h2>
    <ul>
        {#each files.accepted as file}
            <li>{file.name} - {fileSize(file.size)}</li>
        {/each}
    </ul>
    <h2>Rejected files</h2>
    <ul>
        {#each files.rejected as rejected}
            <li>{rejected.file.name} - {rejected.error.message}</li>
        {/each}
    </ul>
{/if}

<pre>{files}</pre>

<style>
    .filedrop {
        background-color: #f0f0f0;
        height: 200px;
        display: flex;
        flex-direction: column;
        align-items: center;
        justify-content: center;
        border-radius: 0.375rem;
        border: 0.7em dashed #c3c3c3;
        outline: 1em solid #f0f0f0;
        transition: border 0.3s ease-in-out;
        outline-offset: -1.3em;
        padding: 0.475em;
    }
    .filedrop:focus {
        border-color: #2196f3;
    }
    .filedrop:hover {
        border-color: #343434;
    }
    .filedrop p,
    .filedrop svg {
        transition: color 0.1s;
        transition: fill 0.1s;
    }
    .filedrop:focus p,
    .filedrop:focus svg {
        color: #2196f3;
        fill: #2196f3;
    }
    .filedrop:hover p,
    .filedrop:hover svg {
        color: #343434;
        fill: #343434;
    }
    p {
        color: #373737;
        font-size: 1.2em;
        cursor: default;
        align-content: center;
    }
</style>
