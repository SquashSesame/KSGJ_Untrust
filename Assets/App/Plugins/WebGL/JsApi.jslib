mergeInto(LibraryManager.library, {

    // Post Tweet
    // "https://blog.gigacreation.jp/entry/2020/10/04/223712"
    TweetFromUnity: function (rawMessage) {
        var message = Pointer_stringify(rawMessage);
        var mobilePattern = /android|iphone|ipad|ipod/i;
        var ua = window.navigator.userAgent.toLowerCase();
        if (ua.search(mobilePattern) !== -1 || (ua.indexOf("macintosh") !== -1 && "ontouchend" in document)) {
            // Mobile
            location.href = "twitter://post?message=" + message;
        } else {
            // PC
            window.open("https://twitter.com/intent/tweet?text=" + message, "_blank");
        }
    },
        
    Reload: function () {
        location.reload();
    },

    SetHiScore: function(score)
    {
        document.cookie = 'hi_score=' + score;
    },

    GetHiScore: function()
    {
        return parseInt(document.cookie.split('=')[1]);
    }, 

    SendGA4TrakEvnt : function (category, label) {

        
    }

});
