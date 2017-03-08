webpackJsonp([1,4],{

/***/ 107:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__browser_info__ = __webpack_require__(520);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return BrowserInfoService; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


var BrowserInfoService = (function () {
    function BrowserInfoService() {
        this.browserInfo = __WEBPACK_IMPORTED_MODULE_1__browser_info__["a" /* BrowserInfo */].parse(navigator.userAgent);
    }
    BrowserInfoService.prototype.getBrowserInfo = function () {
        return this.browserInfo;
    };
    return BrowserInfoService;
}());
BrowserInfoService = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["c" /* Injectable */])(),
    __metadata("design:paramtypes", [])
], BrowserInfoService);

//# sourceMappingURL=browser-info.service.js.map

/***/ }),

/***/ 153:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return Constants; });
var Constants = (function () {
    function Constants() {
    }
    return Constants;
}());

Constants.eventBusEvents = {
    SOMEWHERE_CLICKED: 'somewhere-clicked',
    CRITICAL_ERROR_EVENT_NAME: 'critical-error',
    MENU_ITEM_CLICK: 'menu-item-clicked',
};
Constants.extendedMenuColumnWidth = 200;
//# sourceMappingURL=constants.js.map

/***/ }),

/***/ 154:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__environments_environment__ = __webpack_require__(337);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__angular_http__ = __webpack_require__(311);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__retrievers_menu_items_data_retriever__ = __webpack_require__(522);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__retrievers_users_info_retriever__ = __webpack_require__(523);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return DataProviderService; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};





var DataProviderService = (function () {
    function DataProviderService(http) {
        this.apiUrl = '';
        if (__WEBPACK_IMPORTED_MODULE_1__environments_environment__["a" /* environment */].production) {
            this.apiUrl = location.protocol + "//" + location.host + "/api";
        }
        else {
            this.apiUrl = "http://localhost:5000/api";
        }
        this.http = http;
        this.menuItemsRetriever = new __WEBPACK_IMPORTED_MODULE_3__retrievers_menu_items_data_retriever__["a" /* MenuItemsDataRetriever */](this.apiUrl, http);
        this.usersInfoRetriever = new __WEBPACK_IMPORTED_MODULE_4__retrievers_users_info_retriever__["a" /* UserInfoRetriever */](this.apiUrl, this.http);
    }
    DataProviderService.prototype.menuItemsData = function () {
        return this.menuItemsRetriever;
    };
    DataProviderService.prototype.usersInfo = function () {
        return this.usersInfoRetriever;
    };
    DataProviderService.prototype.currentUserInfo = function () {
        return this.usersInfoRetriever.getCurrentUser();
    };
    return DataProviderService;
}());
DataProviderService = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["c" /* Injectable */])(),
    __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_2__angular_http__["c" /* Http */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_2__angular_http__["c" /* Http */]) === "function" && _a || Object])
], DataProviderService);

var _a;
//# sourceMappingURL=data-provider.service.js.map

/***/ }),

/***/ 334:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__services_message_bus_service_event_bus_service__ = __webpack_require__(66);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__constants__ = __webpack_require__(153);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return ErrorsHandlerComponent; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};



var ErrorsHandlerComponent = ErrorsHandlerComponent_1 = (function () {
    function ErrorsHandlerComponent(eventBus) {
        this.criticalVisibility = false;
        this.criticalTitle = "Critical error occurred!";
        this.criticalBody = "Something went wrong.";
        this.eventBus = eventBus;
        eventBus.subscribe(__WEBPACK_IMPORTED_MODULE_2__constants__["a" /* Constants */].eventBusEvents.CRITICAL_ERROR_EVENT_NAME, ErrorsHandlerComponent_1.criticalErrorsHandler, this);
    }
    ErrorsHandlerComponent.criticalErrorsHandler = function (message, thisContext) {
        thisContext.criticalVisibility = true;
        thisContext.criticalBody = message;
    };
    ErrorsHandlerComponent.warningsHandler = function () {
    };
    ErrorsHandlerComponent.prototype.ngOnInit = function () {
    };
    return ErrorsHandlerComponent;
}());
ErrorsHandlerComponent.CRITICAL_ERROR_EVENT_NAME = 'critical-error';
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["_5" /* ViewChild */])('criticalError'),
    __metadata("design:type", typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_0__angular_core__["C" /* ElementRef */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_0__angular_core__["C" /* ElementRef */]) === "function" && _a || Object)
], ErrorsHandlerComponent.prototype, "criticalErrorElem", void 0);
ErrorsHandlerComponent = ErrorsHandlerComponent_1 = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["_6" /* Component */])({
        selector: 'error',
        template: __webpack_require__(691),
        styles: [__webpack_require__(682)]
    }),
    __metadata("design:paramtypes", [typeof (_b = typeof __WEBPACK_IMPORTED_MODULE_1__services_message_bus_service_event_bus_service__["a" /* EventBusService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__services_message_bus_service_event_bus_service__["a" /* EventBusService */]) === "function" && _b || Object])
], ErrorsHandlerComponent);

var ErrorsHandlerComponent_1, _a, _b;
//# sourceMappingURL=errors-handler.component.js.map

/***/ }),

/***/ 335:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0_rxjs_add_operator_toPromise__ = __webpack_require__(376);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0_rxjs_add_operator_toPromise___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_0_rxjs_add_operator_toPromise__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return DbSet; });

var DbSet = (function () {
    function DbSet(apiUrl, http) {
        this.apiUrl = apiUrl;
        this.http = http;
    }
    DbSet.prototype.getFullUrl = function () {
        return this.apiUrl + "/" + this.urlPath;
    };
    DbSet.prototype.getAll = function (offset, count) {
        if (offset === void 0) { offset = 0; }
        if (count === void 0) { count = 50; }
        return this.http.get(this.getFullUrl() + "/GetAll?offset=" + parseInt(offset.toString()) + "&count=" + parseInt(count.toString()), { withCredentials: true }).toPromise().then(function (response) {
            return response.json();
        }, function (error) {
            throw new Error("Can't download data: " + error.toString());
        });
    };
    DbSet.prototype.saveAll = function (data) {
        return this.http.put(this.getFullUrl(), JSON.stringify(data), { withCredentials: true }).toPromise().then(function (response) {
            return response.ok;
        }, function (error) {
            throw new Error("Can't save data: " + error.toString());
        });
    };
    return DbSet;
}());

//# sourceMappingURL=db-set.js.map

/***/ }),

/***/ 336:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_platform_browser__ = __webpack_require__(104);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return TitleService; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


var TitleService = TitleService_1 = (function () {
    function TitleService(titleSrv) {
        this.title = titleSrv;
    }
    /**
     * Set the title of the current HTML document.
     * @param newTitle
     */
    TitleService.prototype.setTitle = function (newTitle) {
        this.title.setTitle(newTitle + TitleService_1.POSTFIX);
    };
    /**
     * Get the title of the current HTML document.
     * @returns {string}
     */
    TitleService.prototype.getTitle = function () {
        return this.title.getTitle();
    };
    return TitleService;
}());
TitleService.POSTFIX = ' | Математический факультет ЯрГУ';
TitleService = TitleService_1 = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["c" /* Injectable */])(),
    __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_1__angular_platform_browser__["c" /* Title */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__angular_platform_browser__["c" /* Title */]) === "function" && _a || Object])
], TitleService);

var TitleService_1, _a;
//# sourceMappingURL=title.service.js.map

/***/ }),

/***/ 337:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return environment; });
// The file contents for the current environment will overwrite these during build.
// The build system defaults to the dev environment which uses `environment.ts`, but if you do
// `ng build --env=prod` then `environment.prod.ts` will be used instead.
// The list of which env maps to which file can be found in `.angular-cli.json`.
// The file contents for the current environment will overwrite these during build.
var environment = {
    production: false
};
//# sourceMappingURL=environment.js.map

/***/ }),

/***/ 395:
/***/ (function(module, exports) {

function webpackEmptyContext(req) {
	throw new Error("Cannot find module '" + req + "'.");
}
webpackEmptyContext.keys = function() { return []; };
webpackEmptyContext.resolve = webpackEmptyContext;
module.exports = webpackEmptyContext;
webpackEmptyContext.id = 395;


/***/ }),

/***/ 396:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
Object.defineProperty(__webpack_exports__, "__esModule", { value: true });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_platform_browser_dynamic__ = __webpack_require__(483);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__app_app_module__ = __webpack_require__(516);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__environments_environment__ = __webpack_require__(337);




if (__WEBPACK_IMPORTED_MODULE_3__environments_environment__["a" /* environment */].production) {
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["a" /* enableProdMode */])();
}
__webpack_require__.i(__WEBPACK_IMPORTED_MODULE_1__angular_platform_browser_dynamic__["a" /* platformBrowserDynamic */])().bootstrapModule(__WEBPACK_IMPORTED_MODULE_2__app_app_module__["a" /* AppModule */]).then(function () { });
//# sourceMappingURL=main.js.map

/***/ }),

/***/ 514:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_router__ = __webpack_require__(503);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return AppRoutingModule; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};


var routes = [
    {
        path: '',
        children: []
    }
];
var AppRoutingModule = (function () {
    function AppRoutingModule() {
    }
    return AppRoutingModule;
}());
AppRoutingModule = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["b" /* NgModule */])({
        imports: [__WEBPACK_IMPORTED_MODULE_1__angular_router__["a" /* RouterModule */].forRoot(routes)],
        exports: [__WEBPACK_IMPORTED_MODULE_1__angular_router__["a" /* RouterModule */]],
        providers: []
    })
], AppRoutingModule);

//# sourceMappingURL=app-routing.module.js.map

/***/ }),

/***/ 515:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__services_title_service_title_service__ = __webpack_require__(336);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__services_message_bus_service_event_bus_service__ = __webpack_require__(66);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__services_browser_info_service_browser_info_service__ = __webpack_require__(107);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__services_data_provider_service_data_provider_service__ = __webpack_require__(154);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return AppComponent; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};





var PageMode;
(function (PageMode) {
    PageMode[PageMode["Extended"] = 0] = "Extended";
    PageMode[PageMode["FullWidth"] = 1] = "FullWidth";
    PageMode[PageMode["Normal"] = 2] = "Normal";
})(PageMode || (PageMode = {}));
var AppComponent = (function () {
    function AppComponent(title, eventBus, browserInfo, dataProvider) {
        this.pageMode = PageMode.Normal;
        this.sidebarToggled = 'normal';
        this.somewhereClickEventName = 'somewhere-clicked';
        this.titleService = title;
        this.eventBus = eventBus;
        this.dataProvider = dataProvider;
        if (!this.eventBus.eventExists(this.somewhereClickEventName)) {
            this.eventBus.createEvent(this.somewhereClickEventName);
        }
        this.defaultState = browserInfo.getBrowserInfo().isMobile ? 'collapsed' : 'normal';
    }
    AppComponent.prototype.getPageMode = function () {
        switch (this.pageMode) {
            case PageMode.Extended:
                return "extended";
            case PageMode.FullWidth:
                return "full-width";
            case PageMode.Normal:
                return "";
            default:
                throw new Error('Out of range');
        }
    };
    AppComponent.prototype.clickedSomewhere = function ($event) {
        this.eventBus.raise(this.somewhereClickEventName, null, [$event]);
    };
    AppComponent.prototype.ngOnInit = function () {
        var splashScreen = document.getElementById('loading-splash-screen');
        if (splashScreen) {
            $(splashScreen).animate({ opacity: 0 }, 1000, function () {
                $(splashScreen).remove();
            });
        }
        this.titleService.setTitle("Главная страница");
    };
    AppComponent.prototype.toggled = function (data) {
        this.sidebarToggled = data;
    };
    return AppComponent;
}());
AppComponent = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["_6" /* Component */])({
        selector: 'app-root',
        template: __webpack_require__(690),
        styles: [__webpack_require__(681)]
    }),
    __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_1__services_title_service_title_service__["a" /* TitleService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__services_title_service_title_service__["a" /* TitleService */]) === "function" && _a || Object, typeof (_b = typeof __WEBPACK_IMPORTED_MODULE_2__services_message_bus_service_event_bus_service__["a" /* EventBusService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_2__services_message_bus_service_event_bus_service__["a" /* EventBusService */]) === "function" && _b || Object, typeof (_c = typeof __WEBPACK_IMPORTED_MODULE_3__services_browser_info_service_browser_info_service__["a" /* BrowserInfoService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_3__services_browser_info_service_browser_info_service__["a" /* BrowserInfoService */]) === "function" && _c || Object, typeof (_d = typeof __WEBPACK_IMPORTED_MODULE_4__services_data_provider_service_data_provider_service__["a" /* DataProviderService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_4__services_data_provider_service_data_provider_service__["a" /* DataProviderService */]) === "function" && _d || Object])
], AppComponent);

var _a, _b, _c, _d;
//# sourceMappingURL=app.component.js.map

/***/ }),

/***/ 516:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_platform_browser__ = __webpack_require__(104);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_core__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__angular_forms__ = __webpack_require__(474);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__angular_http__ = __webpack_require__(311);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__app_routing_module__ = __webpack_require__(514);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5__app_component__ = __webpack_require__(515);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_6__global_sidebar_global_sidebar_component__ = __webpack_require__(518);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_7__global_content_global_content_component__ = __webpack_require__(517);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_8__services_title_service_title_service__ = __webpack_require__(336);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_9__menu_item_menu_item_component__ = __webpack_require__(519);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_10__services_message_bus_service_event_bus_service__ = __webpack_require__(66);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_11__services_browser_info_service_browser_info_service__ = __webpack_require__(107);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_12__services_data_provider_service_data_provider_service__ = __webpack_require__(154);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_13__errors_handler_errors_handler_component__ = __webpack_require__(334);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_14__user_bar_user_bar_component__ = __webpack_require__(526);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return AppModule; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};















var AppModule = (function () {
    function AppModule() {
    }
    return AppModule;
}());
AppModule = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_1__angular_core__["b" /* NgModule */])({
        declarations: [
            __WEBPACK_IMPORTED_MODULE_5__app_component__["a" /* AppComponent */],
            __WEBPACK_IMPORTED_MODULE_6__global_sidebar_global_sidebar_component__["a" /* GlobalSideBarComponent */],
            __WEBPACK_IMPORTED_MODULE_7__global_content_global_content_component__["a" /* GlobalContentComponent */],
            __WEBPACK_IMPORTED_MODULE_9__menu_item_menu_item_component__["a" /* MenuItemComponent */],
            __WEBPACK_IMPORTED_MODULE_13__errors_handler_errors_handler_component__["a" /* ErrorsHandlerComponent */],
            __WEBPACK_IMPORTED_MODULE_14__user_bar_user_bar_component__["a" /* UserBarComponent */]
        ],
        imports: [
            __WEBPACK_IMPORTED_MODULE_0__angular_platform_browser__["a" /* BrowserModule */],
            __WEBPACK_IMPORTED_MODULE_2__angular_forms__["a" /* FormsModule */],
            __WEBPACK_IMPORTED_MODULE_3__angular_http__["a" /* HttpModule */],
            __WEBPACK_IMPORTED_MODULE_4__app_routing_module__["a" /* AppRoutingModule */],
            __WEBPACK_IMPORTED_MODULE_3__angular_http__["b" /* JsonpModule */]
        ],
        providers: [
            __WEBPACK_IMPORTED_MODULE_8__services_title_service_title_service__["a" /* TitleService */],
            __WEBPACK_IMPORTED_MODULE_10__services_message_bus_service_event_bus_service__["a" /* EventBusService */],
            __WEBPACK_IMPORTED_MODULE_11__services_browser_info_service_browser_info_service__["a" /* BrowserInfoService */],
            __WEBPACK_IMPORTED_MODULE_12__services_data_provider_service_data_provider_service__["a" /* DataProviderService */]
        ],
        bootstrap: [__WEBPACK_IMPORTED_MODULE_5__app_component__["a" /* AppComponent */]]
    })
], AppModule);

//# sourceMappingURL=app.module.js.map

/***/ }),

/***/ 517:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__services_message_bus_service_event_bus_service__ = __webpack_require__(66);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__constants__ = __webpack_require__(153);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__services_browser_info_service_browser_info_service__ = __webpack_require__(107);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return GlobalContentComponent; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};




var GlobalContentComponent = (function () {
    function GlobalContentComponent(eventBus, browserInfo) {
        var _this = this;
        this.sidebarState = 'normal';
        this.defaultState = 'normal';
        this.count = 0;
        this.isMobile = false;
        this.eventBus = eventBus;
        this.isMobile = browserInfo.getBrowserInfo().isMobile;
        eventBus.subscribe(__WEBPACK_IMPORTED_MODULE_2__constants__["a" /* Constants */].eventBusEvents.MENU_ITEM_CLICK, function (active) {
            _this.text.nativeElement.innerHTML = active.item.name;
            _this.clickedText = "Clicked: " + ++_this.count;
        });
    }
    GlobalContentComponent.prototype.ngOnInit = function () {
        this.sidebarState = this.defaultState;
    };
    return GlobalContentComponent;
}());
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["w" /* Input */])(),
    __metadata("design:type", Object)
], GlobalContentComponent.prototype, "sidebarState", void 0);
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["w" /* Input */])(),
    __metadata("design:type", Object)
], GlobalContentComponent.prototype, "defaultState", void 0);
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["_5" /* ViewChild */])('text'),
    __metadata("design:type", typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_0__angular_core__["C" /* ElementRef */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_0__angular_core__["C" /* ElementRef */]) === "function" && _a || Object)
], GlobalContentComponent.prototype, "text", void 0);
GlobalContentComponent = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["_6" /* Component */])({
        selector: 'global-content',
        template: __webpack_require__(692),
        styles: [__webpack_require__(683)],
        animations: [
            __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["_7" /* trigger */])('sidebarState', [
                __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["_8" /* state */])('collapsed', __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["_9" /* style */])({
                    paddingLeft: '58px'
                })),
                __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["_8" /* state */])('normal', __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["_9" /* style */])({
                    paddingLeft: '250px'
                })),
                __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["_10" /* transition */])('collapsed => normal', __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["_11" /* animate */])('100ms ease-in')),
                __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["_10" /* transition */])('normal => collapsed', __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["_11" /* animate */])('100ms ease-in'))
            ])
        ]
    }),
    __metadata("design:paramtypes", [typeof (_b = typeof __WEBPACK_IMPORTED_MODULE_1__services_message_bus_service_event_bus_service__["a" /* EventBusService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__services_message_bus_service_event_bus_service__["a" /* EventBusService */]) === "function" && _b || Object, typeof (_c = typeof __WEBPACK_IMPORTED_MODULE_3__services_browser_info_service_browser_info_service__["a" /* BrowserInfoService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_3__services_browser_info_service_browser_info_service__["a" /* BrowserInfoService */]) === "function" && _c || Object])
], GlobalContentComponent);

var _a, _b, _c;
//# sourceMappingURL=global-content.component.js.map

/***/ }),

/***/ 518:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__services_message_bus_service_event_bus_service__ = __webpack_require__(66);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__services_data_provider_service_data_provider_service__ = __webpack_require__(154);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__errors_handler_errors_handler_component__ = __webpack_require__(334);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return GlobalSideBarComponent; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};




var GlobalSideBarComponent = (function () {
    function GlobalSideBarComponent(eventBus, dataProvider) {
        this.isToggled = new __WEBPACK_IMPORTED_MODULE_0__angular_core__["G" /* EventEmitter */]();
        this.defaultState = 'normal';
        this.menuItems = [];
        var self = this;
        this.eventBus = eventBus;
        this.dataProvider = dataProvider;
        dataProvider.menuItemsData().getAll().then(function (data) {
            self.menuItems = data;
        }, function (error) {
            eventBus.raise(__WEBPACK_IMPORTED_MODULE_3__errors_handler_errors_handler_component__["a" /* ErrorsHandlerComponent */].CRITICAL_ERROR_EVENT_NAME, self, [error.message]);
        });
    }
    GlobalSideBarComponent.prototype.ngOnInit = function () {
        this.userToggled = this.defaultState;
    };
    GlobalSideBarComponent.prototype.ngAfterViewInit = function () {
        var self = this;
        setTimeout(function () {
            $(self.menuBlock.nativeElement).mCustomScrollbar({
                axis: "y",
                theme: "minimal",
                setTop: 0,
                scrollInertia: 200
            });
        });
    };
    GlobalSideBarComponent.prototype.toggled = function () {
        this.userToggled = this.userToggled == 'collapsed' ? 'normal' : 'collapsed';
        this.isToggled.emit(this.userToggled);
    };
    return GlobalSideBarComponent;
}());
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["_2" /* Output */])(),
    __metadata("design:type", Object)
], GlobalSideBarComponent.prototype, "isToggled", void 0);
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["w" /* Input */])(),
    __metadata("design:type", String)
], GlobalSideBarComponent.prototype, "defaultState", void 0);
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["_5" /* ViewChild */])('menuBlock'),
    __metadata("design:type", typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_0__angular_core__["C" /* ElementRef */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_0__angular_core__["C" /* ElementRef */]) === "function" && _a || Object)
], GlobalSideBarComponent.prototype, "menuBlock", void 0);
GlobalSideBarComponent = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["_6" /* Component */])({
        selector: 'global-sidebar',
        template: __webpack_require__(693),
        styles: [__webpack_require__(684)]
    }),
    __metadata("design:paramtypes", [typeof (_b = typeof __WEBPACK_IMPORTED_MODULE_1__services_message_bus_service_event_bus_service__["a" /* EventBusService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__services_message_bus_service_event_bus_service__["a" /* EventBusService */]) === "function" && _b || Object, typeof (_c = typeof __WEBPACK_IMPORTED_MODULE_2__services_data_provider_service_data_provider_service__["a" /* DataProviderService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_2__services_data_provider_service_data_provider_service__["a" /* DataProviderService */]) === "function" && _c || Object])
], GlobalSideBarComponent);

var _a, _b, _c;
//# sourceMappingURL=global-sidebar.component.js.map

/***/ }),

/***/ 519:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__services_message_bus_service_event_bus_service__ = __webpack_require__(66);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__services_browser_info_service_browser_info_service__ = __webpack_require__(107);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__view_models_menu_item_data__ = __webpack_require__(527);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__constants__ = __webpack_require__(153);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return MenuItemComponent; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};





var MenuItemComponent = (function () {
    function MenuItemComponent(eventBus, browserInfo) {
        this.extendedWidth = '0px';
        this.opened = false;
        this.browserInfo = browserInfo;
        var self = this;
        self.eventBus = eventBus;
        eventBus.subscribe(__WEBPACK_IMPORTED_MODULE_4__constants__["a" /* Constants */].eventBusEvents.MENU_ITEM_CLICK, function (activeItem) {
            if (activeItem === self) {
                return;
            }
            self.close();
        });
        this.eventId = eventBus.subscribe(__WEBPACK_IMPORTED_MODULE_4__constants__["a" /* Constants */].eventBusEvents.SOMEWHERE_CLICKED, function ($event) {
            if (self.opened && !($event.toElement.classList.contains('sub-item') ||
                $event.toElement.classList.contains('sub-sub-item') ||
                $event.toElement.classList.contains('menu-content') ||
                $event.toElement.classList.contains('menu-item') ||
                $event.toElement.classList.contains('material-icons'))) {
                self.close();
            }
        });
    }
    MenuItemComponent.prototype.ngOnInit = function () {
        this.extendedWidth = !this.browserInfo.getBrowserInfo().isMobile
            ? __WEBPACK_IMPORTED_MODULE_4__constants__["a" /* Constants */].extendedMenuColumnWidth * this.item.subItems.length + 'px'
            : __WEBPACK_IMPORTED_MODULE_4__constants__["a" /* Constants */].extendedMenuColumnWidth + 'px';
    };
    MenuItemComponent.prototype.ngAfterViewInit = function () {
        if (this.browserInfo.getBrowserInfo().isMobile) {
            var elem = this.extendedMenu.nativeElement;
            if (!elem.classList.contains('mobile')) {
                elem.classList.add('mobile');
            }
        }
    };
    MenuItemComponent.prototype.ngOnDestroy = function () {
        this.eventBus.unsubscribe(this.eventId);
    };
    MenuItemComponent.prototype.clickHandler = function ($event, item, element) {
        if (this.item.subItems && this.item.subItems.length) {
            this.opened = true;
            element.classList.remove('collapsed');
        }
        this.eventBus.raise(__WEBPACK_IMPORTED_MODULE_4__constants__["a" /* Constants */].eventBusEvents.MENU_ITEM_CLICK, this, [this]);
    };
    MenuItemComponent.prototype.close = function () {
        if (this.opened) {
            this.extendedMenu.nativeElement.classList.add('collapsed');
            this.opened = false;
        }
    };
    MenuItemComponent.prototype.getCurrentItems = function (items) {
        return items[0];
    };
    return MenuItemComponent;
}());
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["w" /* Input */])(),
    __metadata("design:type", typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_3__view_models_menu_item_data__["a" /* MenuItemData */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_3__view_models_menu_item_data__["a" /* MenuItemData */]) === "function" && _a || Object)
], MenuItemComponent.prototype, "item", void 0);
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["_5" /* ViewChild */])('extendedMenu'),
    __metadata("design:type", typeof (_b = typeof __WEBPACK_IMPORTED_MODULE_0__angular_core__["C" /* ElementRef */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_0__angular_core__["C" /* ElementRef */]) === "function" && _b || Object)
], MenuItemComponent.prototype, "extendedMenu", void 0);
MenuItemComponent = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["_6" /* Component */])({
        selector: 'menu-item',
        template: __webpack_require__(694),
        styles: [__webpack_require__(685)]
    }),
    __metadata("design:paramtypes", [typeof (_c = typeof __WEBPACK_IMPORTED_MODULE_1__services_message_bus_service_event_bus_service__["a" /* EventBusService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__services_message_bus_service_event_bus_service__["a" /* EventBusService */]) === "function" && _c || Object, typeof (_d = typeof __WEBPACK_IMPORTED_MODULE_2__services_browser_info_service_browser_info_service__["a" /* BrowserInfoService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_2__services_browser_info_service_browser_info_service__["a" /* BrowserInfoService */]) === "function" && _d || Object])
], MenuItemComponent);

var _a, _b, _c, _d;
//# sourceMappingURL=menu-item.component.js.map

/***/ }),

/***/ 520:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__os_type__ = __webpack_require__(521);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return BrowserInfo; });

//some code from: https://github.com/JDMcKinstry/navigator-extensions
var BrowserInfo = (function () {
    function BrowserInfo() {
    }
    BrowserInfo.parse = function (userAgent) {
        var browserInfo = new BrowserInfo();
        browserInfo.name = BrowserInfo.getBrowser(userAgent);
        browserInfo.version = BrowserInfo.getVersion(userAgent).toString();
        browserInfo.isMobile = BrowserInfo.getMobile(userAgent) !== 'Unknown';
        browserInfo.osType = BrowserInfo.getOs(userAgent);
        return browserInfo;
    };
    BrowserInfo.getBrowser = function (userAgent) {
        try {
            switch (true) {
                case (/MSIE|Trident/i.test(userAgent)):
                    return 'MSIE';
                case (/Chrome/.test(userAgent)):
                    return 'Chrome';
                case (/Opera/.test(userAgent)):
                    return 'Opera';
                case (/Kindle|Silk|KFTT|KFOT|KFJWA|KFJWI|KFSOWI|KFTHWA|KFTHWI|KFAPWA|KFAPWI/i.test(userAgent)):
                    return (/Silk/i.test(userAgent)) ? 'Silk' : 'Kindle';
                case (/BlackBerry/.test(userAgent)):
                    return 'BlackBerry';
                case (/PlayBook/.test(userAgent)):
                    return 'PlayBook';
                case (/BB[0-9]{1,}; Touch/.test(userAgent)):
                    return 'Blackberry';
                case (/Android/.test(userAgent)):
                    return 'Android';
                case (/Safari/.test(userAgent)):
                    return 'Safari';
                case (/Firefox/.test(userAgent)):
                    return 'Mozilla';
                case (/Nokia/.test(userAgent)):
                    return 'Nokia';
            }
        }
        catch (err) {
            console.debug("ERROR:setBrowser\t", err);
        }
        return 'Unknown';
    };
    BrowserInfo.getMobile = function (userAgent) {
        try {
            switch (true) {
                case (/Sony[^ ]*/i.test(userAgent)):
                    return 'Sony';
                case (/RIM Tablet/i.test(userAgent)):
                    return 'RIM Tablet';
                case (/BlackBerry/i.test(userAgent)):
                    return 'BlackBerry';
                case (/iPhone/i.test(userAgent)):
                    return 'iPhone';
                case (/iPad/i.test(userAgent)):
                    return 'iPad';
                case (/iPod/i.test(userAgent)):
                    return 'iPod';
                case (/Opera Mini/i.test(userAgent)):
                    return 'Opera Mini';
                case (/IEMobile/i.test(userAgent)):
                    return 'IEMobile';
                case (/BB[0-9]{1,}; Touch/i.test(userAgent)):
                    return 'BlackBerry';
                case (/Nokia/i.test(userAgent)):
                    return 'Nokia';
                case (/Android/i.test(userAgent)):
                    return 'Android';
                case (/Tablet/i.test(userAgent)):
                    return 'Tablet';
                case (/Phone/i.test(userAgent)):
                    return 'Phone';
                case (/Mobile/i.test(userAgent)):
                    return 'Mobile';
                case (/iOS/i.test(userAgent)):
                    return 'iOS';
            }
        }
        catch (err) {
            console.debug("ERROR:setMobile\t", err);
        }
        return 'Unknown';
    };
    BrowserInfo.getVersion = function (userAgent) {
        try {
            switch (true) {
                case (/MSIE|Trident/i.test(userAgent)):
                    if (/Trident/i.test(userAgent) && /rv:([0-9]{1,}[\.0-9]{0,})/.test(userAgent))
                        return parseFloat(userAgent.match(/rv:([0-9]{1,}[\.0-9]{0,})/)[1].replace(/[^0-9\.]/g, ''));
                    return (/MSIE/i.test(userAgent) && parseFloat(userAgent.split("MSIE")[1].replace(/[^0-9\.]/g, '')) > 0)
                        ? parseFloat(userAgent.split("MSIE")[1].replace(/[^0-9\.]/g, ''))
                        : "Edge";
                case (/Chrome/.test(userAgent)):
                    return parseFloat(userAgent.split("Chrome/")[1].split("Safari")[0].replace(/[^0-9\.]/g, ''));
                case (/Opera/.test(userAgent)):
                    return parseFloat(userAgent.split("Version/")[1].replace(/[^0-9\.]/g, ''));
                case (/Kindle|Silk|KFTT|KFOT|KFJWA|KFJWI|KFSOWI|KFTHWA|KFTHWI|KFAPWA|KFAPWI/i.test(userAgent)):
                    if (/Silk/i.test(userAgent))
                        return parseFloat(userAgent.split("Silk/")[1].split("Safari")[0].replace(/[^0-9\.]/g, ''));
                    else if (/Kindle/i.test(userAgent) && /Version/i.test(userAgent))
                        return parseFloat(userAgent.split("Version/")[1].split("Safari")[0].replace(/[^0-9\.]/g, ''));
                case (/BlackBerry/.test(userAgent)):
                    return parseFloat(userAgent.split("/")[1].replace(/[^0-9\.]/g, ''));
                case (/PlayBook/.test(userAgent)):
                case (/BB[0-9]{1,}; Touch/.test(userAgent)):
                case (/Safari/.test(userAgent)):
                    return parseFloat(userAgent.split("Version/")[1].split("Safari")[0].replace(/[^0-9\.]/g, ''));
                case (/Firefox/.test(userAgent)):
                    return parseFloat(userAgent.split(/Firefox\//i)[1].replace(/[^0-9\.]/g, ''));
                case (/Android/.test(userAgent)):
                    return parseFloat(userAgent.split("Version/")[1].split("Safari")[0].replace(/[^0-9\.]/g, ''));
                case (/Nokia/.test(userAgent)):
                    return parseFloat(userAgent.split('Browser')[1].replace(/[^0-9\.]/g, ''));
            }
        }
        catch (err) {
            console.debug("ERROR:setVersion\t", err);
        }
        return -1;
    };
    BrowserInfo.getOs = function (userAgent) {
        switch (true) {
            case /(windows nt 5\.1)|(windows xp)/i.test(userAgent):
            case /windows nt 6\.0/i.test(userAgent):
            case /windows nt 6\.1/i.test(userAgent):
            case /windows nt 6\.2/i.test(userAgent):
            case /windows nt 10\.0/i.test(userAgent):
                return __WEBPACK_IMPORTED_MODULE_0__os_type__["a" /* OsType */].windows;
            case /Android/i.test(userAgent):
                return __WEBPACK_IMPORTED_MODULE_0__os_type__["a" /* OsType */].android;
            case /(iPhone)|(iPad)|(iPod)/i.test(userAgent):
                return __WEBPACK_IMPORTED_MODULE_0__os_type__["a" /* OsType */].iOS;
            case /(linux)|(x11)/i.test(userAgent):
                return __WEBPACK_IMPORTED_MODULE_0__os_type__["a" /* OsType */].linuxBased;
            case /(mac_powerpc)|(macintosh)|(mac)/i.test(userAgent):
                return __WEBPACK_IMPORTED_MODULE_0__os_type__["a" /* OsType */].macOS;
            default:
                return __WEBPACK_IMPORTED_MODULE_0__os_type__["a" /* OsType */].unknown;
        }
    };
    return BrowserInfo;
}());

//# sourceMappingURL=browser-info.js.map

/***/ }),

/***/ 521:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return OsType; });
var OsType;
(function (OsType) {
    OsType[OsType["unknown"] = 0] = "unknown";
    OsType[OsType["windows"] = 1] = "windows";
    OsType[OsType["android"] = 2] = "android";
    OsType[OsType["linuxBased"] = 3] = "linuxBased";
    OsType[OsType["macOS"] = 4] = "macOS";
    OsType[OsType["iOS"] = 5] = "iOS";
})(OsType || (OsType = {}));
//# sourceMappingURL=os-type.js.map

/***/ }),

/***/ 522:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__db_set__ = __webpack_require__(335);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return MenuItemsDataRetriever; });
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();

var MenuItemsDataRetriever = (function (_super) {
    __extends(MenuItemsDataRetriever, _super);
    function MenuItemsDataRetriever() {
        var _this = _super !== null && _super.apply(this, arguments) || this;
        _this.urlPath = "MenuItems";
        return _this;
    }
    MenuItemsDataRetriever.prototype.saveAll = function (data) {
        throw new Error('Not supported!');
    };
    return MenuItemsDataRetriever;
}(__WEBPACK_IMPORTED_MODULE_0__db_set__["a" /* DbSet */]));

//# sourceMappingURL=menu-items-data-retriever.js.map

/***/ }),

/***/ 523:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__db_set__ = __webpack_require__(335);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1_rxjs_add_operator_toPromise__ = __webpack_require__(376);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1_rxjs_add_operator_toPromise___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_1_rxjs_add_operator_toPromise__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return UserInfoRetriever; });
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();


var UserInfoRetriever = (function (_super) {
    __extends(UserInfoRetriever, _super);
    function UserInfoRetriever() {
        var _this = _super !== null && _super.apply(this, arguments) || this;
        _this.urlPath = "UsersInfo";
        return _this;
    }
    UserInfoRetriever.prototype.getCurrentUser = function () {
        return this.http.get(this.getFullUrl() + "/GetCurrentUserInfo", { withCredentials: true }).toPromise().then(function (response) {
            return response.json();
        }, function (error) {
            throw new Error("Can't get current user info. Details: " + error.toString());
        });
    };
    return UserInfoRetriever;
}(__WEBPACK_IMPORTED_MODULE_0__db_set__["a" /* DbSet */]));

//# sourceMappingURL=users-info-retriever.js.map

/***/ }),

/***/ 524:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__handler__ = __webpack_require__(525);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return HandlersStorage; });

var HandlersStorage = (function () {
    function HandlersStorage() {
        this.handlers = [];
    }
    HandlersStorage.prototype.add = function (id, handler, additionalContext) {
        if (additionalContext === void 0) { additionalContext = null; }
        this.handlers.push(new __WEBPACK_IMPORTED_MODULE_0__handler__["a" /* Handler */](id, handler, additionalContext));
    };
    HandlersStorage.prototype.containsId = function (id) {
        var handlersWithSelectedId = this.getHandlerWithId(id);
        return handlersWithSelectedId !== null;
    };
    HandlersStorage.prototype.remove = function (id) {
        var handler = this.getHandlerWithId(id);
        var idx = this.handlers.lastIndexOf(handler);
        this.handlers.splice(idx, 1);
    };
    HandlersStorage.prototype.raise = function (context, args) {
        this.handlers.forEach(function (handler) { return handler.raise(context, args); });
    };
    HandlersStorage.prototype.removeAll = function () {
        this.handlers = [];
    };
    HandlersStorage.prototype.getHandlerWithId = function (id) {
        var handlers = this.handlers.filter(function (handlerId) { return handlerId.getId() == id; });
        if (!handlers)
            throw new Error("There's no handler with id: " + id + ".");
        if (handlers.length != 1)
            throw new Error("Too much handlers with id: " + id + ".");
        return handlers[0];
    };
    return HandlersStorage;
}());

//# sourceMappingURL=handler-storage.js.map

/***/ }),

/***/ 525:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return Handler; });
var Handler = (function () {
    function Handler(id, handler, additionalContext) {
        if (additionalContext === void 0) { additionalContext = null; }
        this.id = id;
        this.handler = handler;
        this.additionalContext = additionalContext;
    }
    Handler.prototype.getId = function () {
        return this.id;
    };
    Handler.prototype.raise = function (context, args) {
        if (this.additionalContext) {
            args.push(this.additionalContext);
        }
        this.handler.apply(context, args);
    };
    return Handler;
}());

//# sourceMappingURL=handler.js.map

/***/ }),

/***/ 526:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__services_data_provider_service_data_provider_service__ = __webpack_require__(154);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__services_browser_info_service_browser_info_service__ = __webpack_require__(107);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__view_models_user_info__ = __webpack_require__(528);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__services_message_bus_service_event_bus_service__ = __webpack_require__(66);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5__constants__ = __webpack_require__(153);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return UserBarComponent; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};






var UserBarComponent = UserBarComponent_1 = (function () {
    function UserBarComponent(dataProvider, browserInfo, eventBus) {
        this.menuVisible = false;
        this.dataProvider = dataProvider;
        this.browserInfo = browserInfo;
        this.eventBus = eventBus;
        var self = this;
        this.user = new __WEBPACK_IMPORTED_MODULE_3__view_models_user_info__["a" /* UserInfo */]();
        dataProvider.currentUserInfo().then(function (user) {
            self.setUserData(user);
        });
        eventBus.subscribe(__WEBPACK_IMPORTED_MODULE_5__constants__["a" /* Constants */].eventBusEvents.SOMEWHERE_CLICKED, UserBarComponent_1.somewhereClickedHandler, this);
    }
    UserBarComponent.prototype.ngOnInit = function () {
    };
    UserBarComponent.prototype.ngAfterViewInit = function () {
        this.menuClose();
    };
    UserBarComponent.prototype.setUserData = function (user) {
        this.login = user.nick;
        this.userName = user.surname + " " + user.name[0] + "." + user.middleName[0] + ".";
        this.user = user;
    };
    UserBarComponent.prototype.toggleMenu = function () {
        if (this.menuVisible) {
            this.menuClose();
        }
        else {
            this.menuOpen();
        }
    };
    UserBarComponent.prototype.menuClose = function () {
        var menuDiv = this.menu.nativeElement;
        var loginTitleDiv = this.loginTitle.nativeElement;
        menuDiv.classList.add('hidden');
        loginTitleDiv.classList.remove('active');
        this.menuVisible = false;
    };
    UserBarComponent.prototype.menuOpen = function () {
        var menuDiv = this.menu.nativeElement;
        var loginTitleDiv = this.loginTitle.nativeElement;
        menuDiv.classList.remove('hidden');
        loginTitleDiv.classList.add('active');
        this.menuVisible = true;
    };
    UserBarComponent.somewhereClickedHandler = function ($event, context) {
        var to = $event.toElement;
        if (!context.menuVisible || to.id === 'user-bar__login')
            return;
        if (to.classList.contains('user-bar__picture') ||
            to.classList.contains('user-bar__user-name') ||
            to.id === 'user-bar__user-menu') {
            return;
        }
        else {
            context.menuClose();
        }
    };
    return UserBarComponent;
}());
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["_5" /* ViewChild */])('userBarWrapper'),
    __metadata("design:type", typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_0__angular_core__["C" /* ElementRef */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_0__angular_core__["C" /* ElementRef */]) === "function" && _a || Object)
], UserBarComponent.prototype, "userBarWrapper", void 0);
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["_5" /* ViewChild */])('userMenu'),
    __metadata("design:type", typeof (_b = typeof __WEBPACK_IMPORTED_MODULE_0__angular_core__["C" /* ElementRef */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_0__angular_core__["C" /* ElementRef */]) === "function" && _b || Object)
], UserBarComponent.prototype, "menu", void 0);
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["_5" /* ViewChild */])('loginTitle'),
    __metadata("design:type", typeof (_c = typeof __WEBPACK_IMPORTED_MODULE_0__angular_core__["C" /* ElementRef */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_0__angular_core__["C" /* ElementRef */]) === "function" && _c || Object)
], UserBarComponent.prototype, "loginTitle", void 0);
UserBarComponent = UserBarComponent_1 = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["_6" /* Component */])({
        selector: 'user-bar',
        template: __webpack_require__(695),
        styles: [__webpack_require__(686)]
    }),
    __metadata("design:paramtypes", [typeof (_d = typeof __WEBPACK_IMPORTED_MODULE_1__services_data_provider_service_data_provider_service__["a" /* DataProviderService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__services_data_provider_service_data_provider_service__["a" /* DataProviderService */]) === "function" && _d || Object, typeof (_e = typeof __WEBPACK_IMPORTED_MODULE_2__services_browser_info_service_browser_info_service__["a" /* BrowserInfoService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_2__services_browser_info_service_browser_info_service__["a" /* BrowserInfoService */]) === "function" && _e || Object, typeof (_f = typeof __WEBPACK_IMPORTED_MODULE_4__services_message_bus_service_event_bus_service__["a" /* EventBusService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_4__services_message_bus_service_event_bus_service__["a" /* EventBusService */]) === "function" && _f || Object])
], UserBarComponent);

var UserBarComponent_1, _a, _b, _c, _d, _e, _f;
//# sourceMappingURL=user-bar.component.js.map

/***/ }),

/***/ 527:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return MenuItemData; });
var MenuItemData = (function () {
    function MenuItemData(icon, name, href, subItems) {
        if (subItems === void 0) { subItems = []; }
        this.icon = icon;
        this.name = name;
        this.href = href;
        this.subItems = subItems;
    }
    return MenuItemData;
}());

//# sourceMappingURL=menu-item-data.js.map

/***/ }),

/***/ 528:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return UserInfo; });
var UserInfo = (function () {
    function UserInfo() {
        this.id = '';
        this.name = '';
        this.surname = '';
        this.middleName = '';
        this.nick = '';
        this.group = null;
    }
    return UserInfo;
}());

//# sourceMappingURL=user-info.js.map

/***/ }),

/***/ 66:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__handler_storage__ = __webpack_require__(524);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return EventBusService; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


var EventBusService = (function () {
    function EventBusService() {
        if (window['__eventBusCount'] && typeof window['__eventBusCount'] === 'number')
            window['__eventBusCount']++;
        else
            window['__eventBusCount'] = 1;
        if (window['__eventBusCount'] > 1)
            throw new Error("There's too much Event Buses. You need to use already created.");
        this.events = {};
        this.lastId = 1;
    }
    /**
     * Checks if event exists.
     * @param {string} eventName Event name.
     *
     * @return {boolean} Returns true if event exists, otherwise - false.
     * */
    EventBusService.prototype.eventExists = function (eventName) {
        return !!this.events[eventName];
    };
    /**
     * Creates event.
     * @param {string} eventName Event name.
     *
     * @return {EventBusService} Current service .
     * */
    EventBusService.prototype.createEvent = function (eventName) {
        if (this.eventExists(eventName))
            throw Error("Event " + eventName + " already exists.");
        this.events[eventName] = new __WEBPACK_IMPORTED_MODULE_1__handler_storage__["a" /* HandlersStorage */]();
        return this;
    };
    /**
     * Allows to subscribe to concrete event.
     * @param {string} eventName Event name.
     * @param {Function} handler Your event handler.
     * @param additionalContext Your additional context.
     * @return {number} Event Id.
     * */
    EventBusService.prototype.subscribe = function (eventName, handler, additionalContext) {
        if (additionalContext === void 0) { additionalContext = null; }
        var eventHandlers = this.events[eventName];
        var id = this.lastId++;
        if (!eventHandlers) {
            this.createEvent(eventName);
            eventHandlers = this.events[eventName];
        }
        eventHandlers.add(id, handler, additionalContext);
        return id;
    };
    /**
     * Raises event with context and args.
     * @param {string} eventName Event name.
     * @param {Object} context Your context for handlers.
     * @param {Array} args Your args for handlers.
     * */
    EventBusService.prototype.raise = function (eventName, context, args) {
        var handlers = this.events[eventName];
        if (handlers) {
            handlers.raise(context, args);
        }
        else {
            throw new Error("There's no event with name " + eventName + ".");
        }
    };
    /**
     * Unsubscribes your single handler or all of your handlers from event.
     * @param {number|string} idOrName Id of your handler or event name.
     * */
    EventBusService.prototype.unsubscribe = function (idOrName) {
        if (typeof idOrName == 'number') {
            var id = idOrName;
            for (var eventName in this.events) {
                var event = this.events[eventName];
                if (event.containsId(id)) {
                    event.remove(id);
                    return;
                }
            }
        }
        else {
            this.events[idOrName].removeAll();
        }
    };
    return EventBusService;
}());
EventBusService = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["c" /* Injectable */])(),
    __metadata("design:paramtypes", [])
], EventBusService);

//# sourceMappingURL=event-bus.service.js.map

/***/ }),

/***/ 681:
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__(57)();
// imports


// module
exports.push([module.i, ".page-wrapper {\n  width: 100%;\n  height: 100%;\n  position: relative; }\n\n#page {\n  font-size: 1em;\n  height: 100%;\n  width: 100%;\n  max-height: 100%;\n  overflow: hidden;\n  position: relative;\n  font-family: 'Roboto', 'Helvetica Neue', 'Segoe UI', 'Arial', sans-serif;\n  margin: auto;\n  box-shadow: 0 0 10px rgba(0, 0, 0, 0.5);\n  display: block;\n  background: transparent;\n  max-width: 1200px; }\n  #page.full-width {\n    max-width: 100%; }\n  #page.extended {\n    margin: inherit;\n    max-width: 1500px; }\n", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ 682:
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__(57)();
// imports


// module
exports.push([module.i, ".critical-error-wrapper {\n  display: none;\n  position: absolute;\n  top: 0;\n  bottom: 0;\n  left: 0;\n  right: 0;\n  z-index: 1000;\n  background-color: rgba(0, 0, 0, 0.85);\n  color: white; }\n  .critical-error-wrapper .critical-error {\n    display: block;\n    max-width: 90%;\n    max-height: 30%;\n    height: 300px;\n    width: 600px;\n    margin: auto;\n    background-color: #6d6d6d;\n    box-shadow: 0 0 5px black;\n    border-radius: 3px; }\n    .critical-error-wrapper .critical-error .critical-title {\n      height: 30px;\n      line-height: 30px;\n      background-color: darkred;\n      border-radius: 3px 3px 0 0;\n      padding: 0 10px;\n      text-transform: uppercase; }\n    .critical-error-wrapper .critical-error .critical-body {\n      padding: 10px; }\n", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ 683:
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__(57)();
// imports


// module
exports.push([module.i, "#right-side {\n  position: relative;\n  height: 100%;\n  display: block;\n  width: 100%;\n  padding-left: 250px; }\n  #right-side #content {\n    width: 100%;\n    height: 100%;\n    background-color: #efefef; }\n\n@media all and (max-width: 500px) {\n  #global-sidebar {\n    width: 68px; }\n  #right-side {\n    padding-left: 58px; } }\n", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ 684:
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__(57)();
// imports


// module
exports.push([module.i, "#global-sidebar {\n  width: 250px;\n  background-color: #1e2050;\n  color: white;\n  float: left;\n  position: relative;\n  height: 100%;\n  display: block;\n  z-index: 10;\n  box-shadow: inset -5px 0px 5px rgba(0, 0, 0, 0.5);\n  -webkit-transition: 200ms;\n  transition: 200ms; }\n  #global-sidebar .logo-wrapper .menu-item {\n    overflow: hidden; }\n  #global-sidebar.collapsed {\n    width: 58px;\n    -webkit-transition: 200ms;\n    transition: 200ms; }\n    #global-sidebar.collapsed .logo-wrapper {\n      width: 58px;\n      -webkit-transition: 200ms;\n      transition: 200ms; }\n    #global-sidebar.collapsed #menu-block {\n      width: 58px;\n      -webkit-transition: 200ms;\n      transition: 200ms; }\n  #global-sidebar.normal {\n    width: 250px;\n    -webkit-transition: 200ms;\n    transition: 200ms; }\n    #global-sidebar.normal .logo-wrapper {\n      width: 250px;\n      -webkit-transition: 200ms;\n      transition: 200ms; }\n    #global-sidebar.normal .menu-item {\n      width: 250px;\n      -webkit-transition: 200ms;\n      transition: 200ms; }\n\n#logo-block {\n  display: block;\n  height: 70px;\n  border-bottom: 1px solid rgba(255, 255, 255, 0.4);\n  padding: 10px 5px;\n  width: 250px;\n  overflow: hidden;\n  overflow-y: auto;\n  position: relative; }\n  #logo-block:hover {\n    background-color: transparent;\n    cursor: default; }\n  #logo-block .icon {\n    display: inline-block;\n    float: left;\n    width: 48px;\n    height: 48px;\n    line-height: 48px;\n    text-align: center;\n    -ms-user-select: none;\n        user-select: none;\n    -moz-user-select: none;\n    -khtml-user-select: none;\n    -webkit-user-select: none;\n    -o-user-select: none; }\n    #logo-block .icon .material-icons {\n      -ms-user-select: none;\n          user-select: none;\n      -moz-user-select: none;\n      -khtml-user-select: none;\n      -webkit-user-select: none;\n      -o-user-select: none;\n      width: 48px;\n      height: 48px;\n      line-height: 48px;\n      text-align: center; }\n  #logo-block .menu-content {\n    height: 48px;\n    line-height: 48px;\n    text-align: left;\n    margin-left: 60px;\n    -ms-user-select: none;\n        user-select: none;\n    -moz-user-select: none;\n    -khtml-user-select: none;\n    -webkit-user-select: none;\n    -o-user-select: none; }\n  #logo-block .menu-extend.collapsed {\n    display: none; }\n\n#logo-text {\n  text-transform: uppercase;\n  cursor: default;\n  font-size: 1.5em; }\n\n.logo-wrapper {\n  overflow: hidden;\n  z-index: 99999;\n  display: block;\n  position: absolute;\n  background-color: #1e2050;\n  box-shadow: inset -5px 0px 5px rgba(0, 0, 0, 0.5); }\n\n#toggle-btn {\n  cursor: pointer; }\n\n#menu-block {\n  height: calc(100% - 70px);\n  top: 70px;\n  position: absolute;\n  width: 250px; }\n  #menu-block:hover {\n    overflow-y: auto; }\n\n@media all and (max-width: 500px) {\n  #left-side {\n    width: 68px; } }\n", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ 685:
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__(57)();
// imports


// module
exports.push([module.i, ".menu-item {\n  min-width: 58px;\n  max-width: 250px;\n  width: 100%;\n  overflow: hidden;\n  overflow-y: visible;\n  display: block;\n  position: relative;\n  padding: 5px; }\n  .menu-item:hover {\n    background-color: #4e4f6b;\n    cursor: pointer; }\n  .menu-item .icon {\n    display: inline-block;\n    float: left;\n    width: 48px;\n    height: 48px;\n    line-height: 48px;\n    text-align: center;\n    -ms-user-select: none;\n        user-select: none;\n    -moz-user-select: none;\n    -khtml-user-select: none;\n    -webkit-user-select: none;\n    -o-user-select: none; }\n    .menu-item .icon .material-icons {\n      -ms-user-select: none;\n          user-select: none;\n      -moz-user-select: none;\n      -khtml-user-select: none;\n      -webkit-user-select: none;\n      -o-user-select: none;\n      width: 48px;\n      height: 48px;\n      line-height: 48px;\n      text-align: center; }\n  .menu-item .menu-content {\n    height: 48px;\n    line-height: 48px;\n    text-align: left;\n    margin-left: 60px;\n    -ms-user-select: none;\n        user-select: none;\n    -moz-user-select: none;\n    -khtml-user-select: none;\n    -webkit-user-select: none;\n    -o-user-select: none; }\n\n.menu-extend {\n  display: block;\n  position: relative;\n  margin-top: -58px;\n  left: 0;\n  min-width: 250px;\n  max-width: inherit;\n  height: 58px;\n  background-color: brown;\n  z-index: 15;\n  overflow: visible;\n  padding: 0; }\n  .menu-extend .sub-items-body {\n    padding: 5px; }\n  .menu-extend .sub-items {\n    background-color: brown;\n    display: -webkit-box;\n    display: -ms-flexbox;\n    display: flex; }\n    .menu-extend .sub-items > ul > li {\n      min-width: 175px; }\n    .menu-extend .sub-items .sub-item {\n      display: block; }\n  .menu-extend.mobile .sub-items {\n    display: block; }\n  .menu-extend.collapsed {\n    display: none; }\n", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ 686:
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__(57)();
// imports


// module
exports.push([module.i, "#user-bar__wrapper {\n  display: block;\n  position: relative;\n  top: 0;\n  width: 100%;\n  background-color: #e8e8e8;\n  height: 30px;\n  line-height: 30px;\n  z-index: 15; }\n  #user-bar__wrapper #user-bar__login {\n    width: auto;\n    min-width: 150px;\n    position: relative;\n    float: right;\n    text-align: center;\n    padding: 0 15px;\n    cursor: pointer;\n    -ms-user-select: none;\n        user-select: none;\n    -moz-user-select: none;\n    -khtml-user-select: none;\n    -webkit-user-select: none;\n    -o-user-select: none; }\n    #user-bar__wrapper #user-bar__login.active {\n      background-color: #1e2050;\n      color: white; }\n      #user-bar__wrapper #user-bar__login.active:hover {\n        background-color: #05092d; }\n    #user-bar__wrapper #user-bar__login:hover {\n      background-color: #d4d4d4; }\n  #user-bar__wrapper #user-bar__user-menu {\n    position: absolute;\n    width: 300px;\n    background: #ccc;\n    top: 30px;\n    right: 0; }\n    #user-bar__wrapper #user-bar__user-menu.hidden {\n      display: none; }\n    #user-bar__wrapper #user-bar__user-menu .user-bar__picture {\n      display: block; }\n    #user-bar__wrapper #user-bar__user-menu .user-bar__user-name {\n      display: block; }\n", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ 690:
/***/ (function(module, exports) {

module.exports = "<error></error>\r\n\r\n<div (click)=\"clickedSomewhere($event)\" class=\"page-wrapper\">\r\n\t<div id=\"page\" [attr.class]=\"getPageMode()\">\r\n\t\t<global-sidebar [defaultState]=\"defaultState\" (isToggled)=\"toggled($event)\"></global-sidebar>\r\n\t\t<global-content [defaultState]=\"defaultState\" [sidebarState]=\"sidebarToggled\"></global-content>\r\n\t</div>\r\n</div>\r\n\r\n<router-outlet></router-outlet>\r\n"

/***/ }),

/***/ 691:
/***/ (function(module, exports) {

module.exports = "<div #criticalError class=\"critical-error-wrapper\" [ngStyle]=\"{'display': criticalVisibility ? 'flex' : 'none'}\">\r\n\t<div class=\"critical-error\">\r\n\t\t<div class=\"critical-title\">\r\n\t\t\t{{ criticalTitle }}\r\n\t\t</div>\r\n\t\t<div class=\"critical-body\">\r\n\t\t\t{{ criticalBody }}\r\n\t\t</div>\r\n\t</div>\r\n</div>\r\n"

/***/ }),

/***/ 692:
/***/ (function(module, exports) {

module.exports = "<div id=\"right-side\" [@sidebarState]=\"sidebarState\" #contentArea>\n\t<div id=\"content\">\n\t\t<user-bar *ngIf=\"!isMobile\"></user-bar>\n\t\t\n\t\t<p>{{ sidebarState }}</p>\n\t\t<p>\n\t\t\tglobal-content works!\n\t\t</p>\n\t\t<p #text></p>\n\t\t<p>{{ clickedText }}</p>\n\t</div>\n</div>\n"

/***/ }),

/***/ 693:
/***/ (function(module, exports) {

module.exports = "<div id=\"global-sidebar\" [class]=\"userToggled\">\n\t<div class=\"logo-wrapper\">\n\t\t<div id=\"logo-block\" class=\"menu-item\">\n\t\t\t<div id=\"toggle-btn\" (click)=\"toggled()\" class=\"icon\">\n\t\t\t\t<i class=\"material-icons md-36\">menu</i>\n\t\t\t</div>\n\t\t\t<div id=\"logo-text\" class=\"menu-content\">\n\t\t\t\tMath Fuck\n\t\t\t</div>\n\t\t</div>\n\t</div>\n\t\n\t<div id=\"menu-block\" #menuBlock>\n\t\t<ul>\n\t\t\t<li *ngFor=\"let item of menuItems\">\n\t\t\t\t<menu-item [item]=\"item\"></menu-item>\n\t\t\t</li>\n\t\t</ul>\n\t</div>\n</div>\n"

/***/ }),

/***/ 694:
/***/ (function(module, exports) {

module.exports = "<div class=\"menu-item\" (click)=\"clickHandler($event, item, extendedMenu)\">\r\n\t<div class=\"icon\">\r\n\t\t<i class=\"material-icons md-30\">{{item.icon}}</i>\r\n\t</div>\r\n\t<div class=\"menu-content\">\r\n\t\t{{item.name}}\r\n\t</div>\r\n</div>\r\n\r\n<div #extendedMenu class=\"menu-item menu-extend collapsed\" [ngStyle]=\"{ 'width': extendedWidth }\">\r\n\t<div class=\"sub-items-body\">\r\n\t\t<div class=\"icon\">\r\n\t\t\t<i class=\"material-icons md-30\">{{item.icon}}</i>\r\n\t\t</div>\r\n\t\t<div class=\"menu-content\">\r\n\t\t\t{{item.name}}\r\n\t\t</div>\r\n\t</div>\r\n\t<div *ngIf=\"item.subItems !== null && item.subItems.length > 0\" class=\"sub-items\">\r\n\t\t<ul *ngFor=\"let columnItems of item.subItems\" [ngStyle]=\"{'width': extendedMenuColumnWidth+'px'}\">\r\n\t\t\t<li *ngFor=\"let subItem of columnItems\" class=\"sub-item\">\r\n\r\n\t\t\t\t{{subItem.name}}\r\n\r\n\t\t\t\t<ul *ngIf=\"subItem.subItems !== null && subItem.subItems.length == 1\">\r\n\t\t\t\t\t<li *ngFor=\"let subSubItem of getCurrentItems(subItem.subItems)\" class=\"sub-sub-item\">\r\n\t\t\t\t\t\t{{ subSubItem.name }}\r\n\t\t\t\t\t</li>\r\n\t\t\t\t</ul>\r\n\t\t\t</li>\r\n\t\t</ul>\r\n\t</div>\r\n</div>\r\n"

/***/ }),

/***/ 695:
/***/ (function(module, exports) {

module.exports = "<div #userBarWrapper id=\"user-bar__wrapper\">\n\t<div #loginTitle id=\"user-bar__login\" (click)=\"toggleMenu()\"> {{ login }} ({{ userName }})</div>\n\t<div #userMenu id=\"user-bar__user-menu\">\n\t\t<div class=\"user-bar__picture\"></div>\n\t\t<div class=\"user-bar__user-name\">{{ user.name + ' ' + user.middleName + ' ' + user.surname }}</div>\n\t\t<ul>\n\t\t\t<li>test_1</li>\n\t\t\t<li>test_2</li>\n\t\t\t<li>test_3</li>\n\t\t</ul>\n\t</div>\n</div>\n"

/***/ }),

/***/ 713:
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__(396);


/***/ })

},[713]);
//# sourceMappingURL=main.bundle.js.map