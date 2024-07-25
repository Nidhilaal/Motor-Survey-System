export class MotorClmSurHdrEntity {
    constructor(
        public surUid: number = 0,
        public surclmUid: number = 0,
        public surclmNo: string = '',
        public surNo: string = '',
        public surChassisNo: string = '',
        public surRegnNo: string = '',
        public surEngineNo: string = '',
        public surCurr: string = '',
        public surFcAmt: number | null = null,
        public surLcAmt: number | null = null,
        public surStatus: string = '',
        public surApprSts: string = '',
        public surApprDt: Date = new Date(),
        public surApprBy: string = '',
        public surCrBy: string = '',
        public surCrDt: Date = new Date(),
        public surUpBy: string = '',
        public surUpDt: Date = new Date()
    ) {}
}
