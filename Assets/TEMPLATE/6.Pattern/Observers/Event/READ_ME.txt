Hướng dẫn dùng Monster Observer pattern

I. MonsterEventManager class
- Là singleton monobehavior để control các event được sử dụng trong game.
- Bắt buộc phải có mặt trên scene.
- Khai báo event bằng code 
	1. [SerializeField] private MonsterEvent<T> mEvent = new MonsterEvent<T>();
	2. public static MonsterEvent<T> MEvent => instance.mEvent;
	// 1. Thay T bằng value type bất kì, đã test với int, float, enum, string và object.
	      giữ reference của event để debug. Khi chạy trên editor có thêm các field để show ref
	      đến các listener và lịch sử post event
	//2. Dòng 2 để tạo quick access code cho nhanh, k có cũng chạy được.

II. MonsterEvent class
- Class này gồm 3 method chính của 1 event là Post, AddListener và RemoveListener
- Class chứa field để hiển thị history và listener - only Unity Editor
* AddListener method
	- Đăng kí nghe event
	- Nếu chạy trên editor: gắn ref của listener để debug
* RemoveListener method
	- Ngừng nghe event
	- Không phải remove ngay lập tức mà sẽ lưu tạm listener vào 1 set phụ, sẽ remove thật ở
	lần post event kế tiếp nên có thể gọi remove bất cứ lúc nào kể cả trong vòng callback
* Post method
	- kiểm tra xem có listener nào đã trong hàng đợi remove/ nếu có thì remove
	- Invoke các listener callback hiện tại

III. USING
  Step 1: Tạo gameObject trên scene, gắn component MonsterEventManager.
  Step 2: Tạo event mới trong MonsterEventManager bằng cách thêm cái đoạn code mẫu
	[SerializeField] private MonsterEvent<T> mEvent = new MonsterEvent<T>();
	public static MonsterEvent<T> MEvent => instance.mEvent;
		// 1. Thay T bằng value type bất kì, đã test với int, float, enum, string và object.
	      	giữ reference của event để debug. Khi chạy trên editor có thêm các field để show ref
	      	đến các listener và lịch sử post event
		//2. Dòng 2 để tạo quick access code cho nhanh, k có cũng chạy được.
  Step 3: Post event
	ở class trigger event thêm cái code này:
		MonsterEventManager.MEvent.Post(this, intValue);
	- MEvent là cái event vừa đc tạo ở bước trước
	- param đầu để xác định component nào đang trigger event, tốt nhất là đừng để null.
	- param thứ 2 để post kèm theo value nếu listener dùng đến
  Step 4: AddListener
	ở class listener thêm cái code này để bắt đầu nghe event
		MonsterEventManager.MEvent.AddListener(this, OnListened);
		chuột phải vào cái OnListened để gen method tương ứng với value type của event
		//Thường đặt AddListener ở Start
  Step 5: RemoveListener
		MonsterEventManager.MEvent.RemoveListener(this, OnListened);
IV. Các lưu ý khác
- Trường hợp 1 class nghe cùng 1 event với cùng 1 callback nhiều lần: 
	chỉ trigger 1 lần mỗi khi event được post
- Trường hợp 1 class nghe cùng 1 event nhưng với 2 callback khác nhau:
	trigger cả 2 callback
	