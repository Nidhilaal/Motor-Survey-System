export class MotorClmSurDtlEntity {
    constructor(
        public surdUid: number = 0,
        public surdSurUid: number = 0,
        public surdItemCode: string = '',
        public surdType: string = '',
        public surdUnitPrice: number = 0,
        public surdQuantity: number = 0,
        public surdFcAmt: number = 0,
        public surdLcAmt: number = 0,
        public surdCrBy: string = '',
        public surdCrDt: Date = new Date(),
        public surdUpBy: string = '',
        public surdUpDt: Date = new Date()
    ) {}
}
