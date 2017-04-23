(function () {
    var perfHub = $.connection.perfHub;
    $.connection.hub.logging = true;
    $.connection.hub.start();

    perfHub.client.newMessage = function (message) {
        model.addMessage(message);
    }

    perfHub.client.newCounters = function (counters) {
        model.addCounters(counters);
    }

    var ChartEntry = function (name) {
        var self = this;
        self.name = name;
        self.chart = new SmoothieChart({ millisPerPixel: 50, labels: { fontsize: 15 } });
        self.timeSeries = new TimeSeries();
        self.chart.addTimeSeries(self.timeSeries, { lineWidth: 3, strokeStyle: "#00ff00" });
    }

    ChartEntry.prototype = {
        addValue: function (value) {
            var self = this;
            self.timeSeries.append(new Date().getTime(), value);
        },
        start: function () {
            var self = this;
            self.canvas = document.getElementById(self.name);
            self.chart.streamTo(self.canvas);
        }

    }

    var Model = function () {
        var self = this;
        self.message = ko.observable("");
        self.messages = ko.observableArray();
        self.counters = ko.observableArray();
    }

    Model.prototype = {
        sendMessage: function () {
            var self = this;
            perfHub.server.send(self.message());
            self.message("");
        },
        addMessage: function (message) {
            var self = this;
            self.messages.push(message);
        },
        addCounters: function (counters) {
            var self = this;

            $.each(counters, function (index, counter) {
                var entry = ko.utils.arrayFirst(self.counters(), function (existCounter) {
                    return existCounter.name == counter.name;
                })

                if (!entry) {
                    entry = new ChartEntry(counter.name);
                    self.counters.push(entry);
                    entry.start();
                }
                entry.addValue(counter.value);
            });
        }
    };

    var model = new Model();

    $(function () {
        ko.applyBindings(model);
    })
}())