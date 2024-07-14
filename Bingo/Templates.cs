using Scriban;

namespace Bingo;

public class Templates
{
    public static Template MainTemplate()
    {
        return Template.Parse(@"
<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <title>GIGA BINGO</title>
    <script src=""https://unpkg.com/htmx.org@1.9.12""></script>
    <script src=""https://unpkg.com/htmx.org/dist/ext/json-enc.js""></script>
    <link href=""https://cdn.jsdelivr.net/npm/daisyui@4.12.10/dist/full.min.css"" rel=""stylesheet"" type=""text/css"" />
    <script src=""https://cdn.tailwindcss.com""></script>
</head>
<script>
    function copyHref() {
        console.log(window.location.href);
        navigator.clipboard.writeText(window.location.href);
    }
</script>
<body>
    <form 
        hx-get=""/board""
        hx-target=""body""
        hx-swap=""outerHTML""
    >
        <div class=""card bg-base-100 w-96 shadow-xl m-8"">
            <div class=""card-body"">
                <h2 class=""card-title"">Dołącz do istniejącego pokoju</h2>
                <div class=""card-actions justify-end"">
                    <label class=""input input-bordered input-primary flex items-center gap-2"">
                        Nr pokoju
                        <input type=""text"" name=""board"" />
                    </label>
                    <button 
                        class=""btn btn-primary""
                        type=""submit"" value=""Submit"">Dołącz do pokoju</button>
                </div>
                </div>
        </div>
    </form>

    <form 
        hx-post=""/board""
        hx-target=""body""
        hx-swap=""outerHTML""
        hx-ext=""json-enc""
    >
        <div class=""card bg-base-100 w-10/12 shadow-xl m-8"">
            <div class=""card-body"">
                <h2 class=""card-title"">Utwórz nowy pokój</h2>
                <input class=""input input-bordered input-primary"" type=""number"" name=""size"" value=""{{size}}"" min=""3"" max=""6"" 
                    hx-get=""/""
                    hx-target=""body""
                    hx-swap=""outerHTML""
                    hx-trigger=""change""
                />
                <input class=""input input-bordered input-primary"" type=""text"" name=""name"" placeholder=""Nazwa pokoju"" />
                <div class=""card-actions justify-end"">
                    <table>
                        {{ for i in (1..size) }}
                        <tr>
                            {{ for j in (1..size) }}
                                <th class=""w-24"">
                                    <input class=""input input-bordered input-primary"" type=""text"" name=""values"" />
                                </th>
                            {{ end }}
                        </tr>
                        {{ end }}
                    </table>
                    <button 
                        class=""btn btn-primary""
                        type=""submit"" value=""Submit"">Utwórz pokój</button>
                </div>
                </div>
        </div>
    </form>
</body>
");
    }
    
    public static Template BoardTemplate()
    {
        return Template.Parse(@"
<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <title>GIGA BINGO</title>
    <script src=""https://unpkg.com/htmx.org@1.9.12""></script>
    <script src=""https://unpkg.com/htmx.org/dist/ext/json-enc.js""></script>
    <link href=""https://cdn.jsdelivr.net/npm/daisyui@4.12.10/dist/full.min.css"" rel=""stylesheet"" type=""text/css"" />
    <script src=""https://cdn.tailwindcss.com""></script>
</head>
<script>
    function copyHref() {
        console.log(window.location.href);
        navigator.clipboard.writeText(window.location.href);
    }
</script>
<body>
    <div
        hx-get=""/board?board={{ room }}""
        hx-target=""body""
        hx-swap=""outerHTML""
        hx-trigger=""every 2.5s""
>
        <h1 class=""text-3xl"">{{ name }}</h1>
        <h2 class=""text-xl"">Pokój: {{ room }}</h2>
        <button class=""btn btn-primary"" onclick=""copyHref()"">Kopiuj link</button>
        <table>
        {{ for column in fields }}
            <tr>
            {{ for row in column }}
                <th>
                <button class=""btn text-md w-48 h-24 border-2 border-black m-1 {{ row.is_marked ? 'btn-error' : 'btn-primary' }}"" name=""field-{{ row.id }}""
                    hx-post=""/board/{{ room }}/{{ row.id }}""
                    hx-target=""body""
                    hx-swap=""outerHTML""
                    hx-trigger=""click""
                    >
                    {{ row.value }}
                </button>
                </th>
            {{ end }}
            </tr>
        {{ end }}
        </table>
    </div>
</body>
</html>
");
    }
    
    public static Template NotFound()
    {
        return Template.Parse(@"
<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <title>GIGA BINGO</title>
    <script src=""https://unpkg.com/htmx.org@1.9.12""></script>
    <script src=""https://unpkg.com/htmx.org/dist/ext/json-enc.js""></script>
    <link href=""https://cdn.jsdelivr.net/npm/daisyui@4.12.10/dist/full.min.css"" rel=""stylesheet"" type=""text/css"" />
    <script src=""https://cdn.tailwindcss.com""></script>
</head>
<body>
    <div>
        404 NOT FOUND
    </div>
</body>
</html>
");
    }
    
    public static Template InternalErr()
    {
        return Template.Parse(@"
<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <title>GIGA BINGO</title>
    <script src=""https://unpkg.com/htmx.org@1.9.12""></script>
    <script src=""https://unpkg.com/htmx.org/dist/ext/json-enc.js""></script>
    <link href=""https://cdn.jsdelivr.net/npm/daisyui@4.12.10/dist/full.min.css"" rel=""stylesheet"" type=""text/css"" />
    <script src=""https://cdn.tailwindcss.com""></script>
</head>
<body>
    <div>
        500 INTERNAL SERVER ERROR
    </div>
</body>
</html>
");
    }
}
