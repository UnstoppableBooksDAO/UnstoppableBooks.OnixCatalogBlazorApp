async function openCacheStorage()
{
    return await window.caches.open("ONIX Catalog");
}

function createRequest(url, method, body = "")
{
    let requestInit = {
        method: method
    };
    if (body != "") {
        requestInit.body = body;
    }
    let request = new Request(url, requestInit);
    console.log(request);
    return request;
}

//In your JavaScript module, add functions to store, get, delete the data:
export async function store(url, method, body = "", responseString)
{
    let catalogBlazorCache = await openCacheStorage();
    let request = createRequest(url, method, body);
    let response = new Response(responseString);
    await catalogBlazorCache.put(request, response);
}

export async function get(url, method, body = "")
{
    let catalogBlazorCache = await openCacheStorage();
    let request = createRequest(url, method, body);
    let response = await catalogBlazorCache.match(request);
    if (response == undefined)
    {
        return "";
    }
    let result = await response.text();
    return result;
}

export async function getKeys()
{
    const keys = [];

    let catalogBlazorCache = await openCacheStorage();
    let requests = await catalogBlazorCache.keys();

    for (let i = 0; i < requests.length; i++)
    {
        keys.push(requests[i].url);
    }

    return keys;
}

export async function remove(url, method, body = "")
{
    let catalogBlazorCache = await openCacheStorage();
    let request = createRequest(url, method, body);
    await catalogBlazorCache.delete(request);
}

export async function removeAll()
{
    let catalogBlazorCache = await openCacheStorage();
    let requests = await catalogBlazorCache.keys();
    for (let i = 0; i < requests.length; i++)
    {
        await catalogBlazorCache.delete(requests[i]);
    }
}