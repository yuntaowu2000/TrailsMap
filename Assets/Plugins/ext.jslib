mergeInto(LibraryManager.library, 
{
    OpenWebPage: function(link) {
        window.open(UTF8ToString(link), '_blank');
    }
});