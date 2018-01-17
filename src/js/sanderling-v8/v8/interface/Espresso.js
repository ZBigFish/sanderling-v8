define('sanderling-v8/v8/interface/Espresso', [], function() {

var EspressoV8Interface = function(v8AppInterface) {
    if (EspressoV8Interface._singleton !== null) {
        throw new Error('Espresso V8 interface already initialized.');
    }

    this._v8AppInterface = v8AppInterface;
};

EspressoV8Interface.VERSION = "0.0.0";

EspressoV8Interface._singleton = null;

EspressoV8Interface.singleton = function() {
    if (EspressoV8Interface._singleton === null) {
        throw new Error('Espresso V8 interface not initialized yet.');
    }

    return EspressoV8Interface._singleton;
};

EspressoV8Interface.initialize = function(v8AppInterface) {
    if (EspressoV8Interface._singleton !== null) {
        throw new Error('Espresso V8 interface already initialized.');
    }

    EspressoV8Interface._singleton = new EspressoV8Interface(v8AppInterface);

    return EspressoV8Interface._singleton;
};

EspressoV8Interface.prototype = Object.create(EspressoV8Interface);

EspressoV8Interface.prototype.getVersion = function() {
    return EspressoV8Interface.VERSION;
};

return EspressoV8Interface;
});
