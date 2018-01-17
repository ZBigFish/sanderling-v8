define('sanderling-v8/bootstrap', [
    'sanderling-v8/v8/interface/Espresso' ],
function (
    EspressoV8Interface ) {

    var bootstrap = function() {

    };

    bootstrap.init = function (v8AppInterface) {
        EspressoV8Interface.initialize(v8AppInterface);
    };
});