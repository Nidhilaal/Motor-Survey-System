export class MotorClaimEntity{
    constructor(
        public clmUid: number = 0,
        public clmNo: string = '',
        public clmPolNo: string = '',
        public clmPolFmDt: Date = new Date(),
        public clmPolToDt: Date = new Date(),
        public clmLossDt: Date = new Date(),
        public clmIntmDt: Date = new Date(),
        public clmRegDt: Date = new Date(),
        public clmPolRepNo: string = '',
        public clmLossDesc: string = '',
        public clmVehChassisNo: string = '',
        public clmVehEngineNo: string = '',
        public clmVehRegnNo: string = '',
        public clmVehValue: number = 0,
        public clmSurCrYn: string = '',
        public clmSurApprYn: string = '',
        public clmSurNo: string = '',
        public clmCrBy: string = '',
        public clmCrDt: Date = new Date(),
        public clmUpBy: string = '',
        public clmUpDt: Date = new Date()
    ) {}
}