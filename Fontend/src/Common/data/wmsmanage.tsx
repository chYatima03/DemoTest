
const InboundWMSData = [
    { id: "1", no: "", documenttypeno: "", documenttypename: "", date: "", deliverydate: "", deliveryno: "", inWmsno: "", inWmsname: "" }
];
const InboundDetailWMSData = [
    { id: "", inboundid: "", lotno: "", rmno: "", rmweigth: "", rmqty: "", unit: "", storageLocation: "" }
];
//Transfer
const TransferWMSData = [
    { id: "1", no: "T67010A2900001", documenttypeno: "RL", documenttypename: "ใบย้ายสินค้า", outStore: "P1-FG", InStore: "P2-FG", date: "29/02/2567", by: ""  }
];
const TransferDetailWMSData = [
    { id: "1", transferid: "1", no: "COM0001", name: "Memory", qty: "12", unit: "pcs.", outWmsno: "P1-FG-1-2-2-4-56", outWmsname: "", inWmsno: "P2-FG-4-2-2-8-10", inWmsname: "" , date: "29/02/2567", by: "" }
];

// const OutboundWMSData = [
//     {id: "", no:"", documenttypeno:"", documenttypename:"", date:"",outWmsno:"", outWmsname:"", inWmsno:"", inWmsname:"" }
// ];
// const OutboundDetailWMSData = [
//     {id:"", outboundid:"", lotno:"", rmno:"", rmsize:"", rmweigth:"", rmqty:"",  unit:"", storageLocation:""}
// ];

//stock list
const StockListData = [
    { id: "1", no: "", lotno: "", name: "", qty: "", unit: "", expiredate: "", currentwmsno: "", status: "pending Hold", date: "29/02/2567", by: "" },
    { id: "2", no: "", lotno: "", name: "", qty: "", unit: "", expiredate: "", currentwmsno: "", status: "pending Hold", date: "29/02/2567", by: "" },
    { id: "3", no: "", lotno: "", name: "", qty: "", unit: "", expiredate: "", currentwmsno: "", status: "pending Hold", date: "29/02/2567", by: "" },
    { id: "4", no: "", lotno: "", name: "", qty: "", unit: "", expiredate: "", currentwmsno: "", status: "pending Hold", date: "29/02/2567", by: "" },
    { id: "5", no: "", lotno: "", name: "", qty: "", unit: "", expiredate: "", currentwmsno: "", status: "pending Hold", date: "29/02/2567", by: "" },
    { id: "6", no: "", lotno: "", name: "", qty: "", unit: "", expiredate: "", currentwmsno: "", status: "pending Hold", date: "29/02/2567", by: "" },
    { id: "7", no: "", lotno: "", name: "", qty: "", unit: "", expiredate: "", currentwmsno: "", status: "pending Hold", date: "29/02/2567", by: "" },
    { id: "8", no: "", lotno: "", name: "", qty: "", unit: "", expiredate: "", currentwmsno: "", status: "pending Hold", date: "29/02/2567", by: "" },
    { id: "9", no: "", lotno: "", name: "", qty: "", unit: "", expiredate: "", currentwmsno: "", status: "pending Hold", date: "29/02/2567", by: "" },
    { id: "10", no: "", lotno: "", name: "", qty: "", unit: "", expiredate: "", currentwmsno: "", status: "pending Hold", date: "29/02/2567", by: "" },
    { id: "11", no: "", lotno: "", name: "", qty: "", unit: "", expiredate: "", currentwmsno: "", status: "pending Hold", date: "29/02/2567", by: "" },
]
//Data Master
const DocumentsData = [
    { id: "1", docno: "RL", docname: "ใบย้ายสินค้า", status: "Open", date: "29/02/2567", by: "" },
    { id: "2", docno: "RI", docname: "ใบรับสินค้า", status: "Open", date: "29/02/2567", by: "" },
    { id: "3", docno: "RO", docname: "ใบจ่ายสินค้า", status: "Open", date: "29/02/2567", by: "" },
    { id: "4", docno: "RN", docname: "ใบตรวจนับสินค้า", status: "Open", date: "29/02/2567", by: "" },
];
const PlanManageWMSData = [
    { id: "1", planno: "P1", planname: "Pan New", status: "Open" , date: "29/02/2567", by: "" },
    { id: "2", planno: "P2", planname: "Pan 2", status: "close" , date: "29/02/2567", by: "" },
];
const StorageLocationWmsData = [
    { id: "1", no: "FG", name: "คลังสินค้า FG" , date: "29/02/2567", by: "" },
    { id: "2", no: "BK1", name: "คลังรอจำหน่าย" , date: "29/02/2567", by: "" },
];
const LocationManageWMSData = [
    { id: "1", planid: "1", planno: "pn1", planname: "Plan 1", storageno: "FG", storagename: "คลังสินค้า FG", zone: "A", layer: "1", road: "1", column: "1", row: "2", position: "1", status: "Open" , date: "29/02/2567", by: ""  },
    { id: "2", planid: "1", planno: "pn1", planname: "Plan 1", storageno: "BK1", storagename: "คลังรอจำหน่าย", zone: "A", layer: "1", road: "1", column: "1", row: "2", position: "2", status: "Close" , date: "29/02/2567", by: "" }
];

export { StockListData, DocumentsData, PlanManageWMSData, LocationManageWMSData, InboundWMSData, InboundDetailWMSData, TransferWMSData, TransferDetailWMSData, StorageLocationWmsData }